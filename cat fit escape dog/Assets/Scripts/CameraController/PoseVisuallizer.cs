using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Mediapipe.BlazePose;
using UnityEngine.SceneManagement;

public class PoseVisuallizer : MonoBehaviour
{
    [SerializeField] GameObject Visuallizer;
    [SerializeField] GameObject Action_Threshold;
    [SerializeField] WebCamInput webCamInput;
    [SerializeField] RawImage inputImageUI;
    [SerializeField] GameObject canvas;
    [SerializeField] Shader shader;
    [SerializeField, Range(0, 1)] float humanExistThreshold = 0.8f;
    [SerializeField, Range(0, 1)] float handjoinThreshold = 0.8f;
    // for pose confirm page
    [SerializeField] GameObject startbutton;
    [SerializeField] GameObject instrction_text;
    [SerializeField] GameObject Pausebutton;
    [SerializeField] GameObject Handjoinbar;

    Material material;
    BlazePoseDetecter detecter;

    Vector4 left,right;
    float height, screen_h;
    RectTransform rectTransform;
    RectTransform action_rectTransform;
    RectTransform CamView_rectTransform;

    public static float action_thredshold = 0.5f;
    PauseMenu pausescript;
    float count_pause = 0;
    Slider handjoinbar;

    public static string input_pose;

    
    

    // Lines count of body's topology.
    const int BODY_LINE_NUM = 35;
    // Pairs of vertex indices of the lines that make up body's topology.
    // Defined by the figure in https://google.github.io/mediapipe/solutions/pose.
    readonly List<Vector4> linePair = new List<Vector4>{
        new Vector4(0, 1), new Vector4(1, 2), new Vector4(2, 3), new Vector4(3, 7), new Vector4(0, 4), 
        new Vector4(4, 5), new Vector4(5, 6), new Vector4(6, 8), new Vector4(9, 10), new Vector4(11, 12), 
        new Vector4(11, 13), new Vector4(13, 15), new Vector4(15, 17), new Vector4(17, 19), new Vector4(19, 15), 
        new Vector4(15, 21), new Vector4(12, 14), new Vector4(14, 16), new Vector4(16, 18), new Vector4(18, 20), 
        new Vector4(20, 16), new Vector4(16, 22), new Vector4(11, 23), new Vector4(12, 24), new Vector4(23, 24), 
        new Vector4(23, 25), new Vector4(25, 27), new Vector4(27, 29), new Vector4(29, 31), new Vector4(31, 27), 
        new Vector4(24, 26), new Vector4(26, 28), new Vector4(28, 30), new Vector4(30, 32), new Vector4(32, 28)
    };

    public void changetoingame(){
        GlobalParameter.gamemode = 1;
        // call reset camera
        SceneManager.LoadScene(2/*game scene*/);
        // SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    bool check_pause(float timer){
        /*
        check pause menu change by using hand joined together
        Input: time require to hold for changing mode
        Output: action to pause
        */
        Vector4 l_hand = detecter.GetPoseLandmark(15);
        Vector4 r_hand = detecter.GetPoseLandmark(16);
        if (r_hand.w > humanExistThreshold && l_hand.w > humanExistThreshold){
            float eclidean_dis = (float) Math.Sqrt(Math.Pow(l_hand.y - r_hand.y, 2) + Math.Pow(l_hand.x - r_hand.x, 2));
            Debug.LogFormat("count :{0} {1}", count_pause, eclidean_dis);
            if ((eclidean_dis > 0.60) && (l_hand.y - r_hand.y < 0.01f) ){
                // hand joined count timer
                // TODO: visualize progress bar
                handjoinbar.value = count_pause / timer;
                count_pause += Time.deltaTime;
                
                Debug.LogFormat("count :{0} {1}", count_pause, eclidean_dis);
            } else{
                count_pause = 0;
            }
            if (count_pause >= timer){
                // fix new action_thredshold
                // reset counter
                count_pause = 0;
                // Debug.LogFormat("{0} {1}", "changed mode", action_thredshold);
                return true;
            }
        }else{
            count_pause = 0;
        }
        return false;
    }

    string CheckAction(float h){
        /*
        this function use to detect action used in game ( jump / slide / Idle)
        Input: current height of player
        Output: current pose 
         */
        float upper_bound = action_thredshold + 0.10f;
        float lowwer_bound = action_thredshold - 0.05f;
        if (h >= upper_bound){
            return "jump";
        }
        else if (h <= lowwer_bound){
            return "slide";
        }
        else{
            return "run";
        }
    }

    void Start(){
        material = new Material(shader);
        detecter = new BlazePoseDetecter();
        rectTransform = Visuallizer.GetComponent<RectTransform>();
        action_rectTransform = Action_Threshold.GetComponent<RectTransform>();
        CamView_rectTransform = canvas.GetComponent<RectTransform>();
        try{pausescript = Pausebutton.GetComponent<PauseMenu>();}
        catch{}
        count_pause = 0;
        handjoinbar = Handjoinbar.GetComponent<Slider>();
        // rectTransform.transfor
    }

    void LateUpdate(){
        if (detecter == null) detecter = new BlazePoseDetecter();
        inputImageUI.texture = webCamInput.inputImageTexture;

        // Predict pose by neural network model.
        try{
            detecter.ProcessImage(webCamInput.inputImageTexture);
        }catch{
            return;
        }
        
        left = detecter.GetPoseLandmark(11);
        right = detecter.GetPoseLandmark(12);

        bool ChangeMode = check_pause(0.5f);
        /*
        changing scene
        */

        if (ChangeMode){
            // from start menu
            if (GlobalParameter.gamemode == 0){
                // check scene if in pose scene then do this
                try{
                     // show button for continue
                    startbutton.SetActive(true);
                    // load in game scene
                    instrction_text.SetActive(false);
                    action_thredshold = (right.y + left.y)/2;
                }catch{

                }
            //from in game scene
            } else if (GlobalParameter.gamemode == 1){
                // go to pause scene
                pausescript.Invoke("Pause", 0.1f);
            }
        }

        /*
        rendering zone
        */
        if (right.w >= humanExistThreshold && left.w >= humanExistThreshold){
            /*
            tracking bar
            */

            // find mean relative height
            height = (right.y + left.y)/2;
            input_pose = CheckAction(height);
            // Debug.LogFormat("pose: {0}",input_pose);
            // update position panel ( y)
            screen_h = inputImageUI.rectTransform.rect.height;
            // update bar position for visualization
            Vector3 pos = new Vector3(
                CamView_rectTransform.rect.width - inputImageUI.rectTransform.rect.width/2,
                screen_h * height + CamView_rectTransform.position.y - screen_h/2,
                0
            );
            rectTransform.position = pos;

            /*
            action thredshold bar
            */

            Vector3 action_pos = new Vector3(
                CamView_rectTransform.rect.width - inputImageUI.rectTransform.rect.width/2,
                action_thredshold * screen_h + inputImageUI.rectTransform.position.y - screen_h/2,
                0
            );
            action_rectTransform.position = action_pos;
        }
        

        // Output landmark values(33 values) and the score whether human pose is visible (1 values).
        // for(int i = 0; i < detecter.vertexCount + 1; i++){
        //     /*
        //     0~32 index datas are pose landmark.
        //     Check below Mediapipe document about relation between index and landmark position.
        //     https://google.github.io/mediapipe/solutions/pose#pose-landmark-model-blazepose-ghum-3d
        //     Each data factors are
        //     x: x cordinate value of pose landmark ([0, 1]).
        //     y: y cordinate value of pose landmark ([0, 1]).
        //     z: Landmark depth with the depth at the midpoint of hips being the origin.
        //        The smaller the value the closer the landmark is to the camera. ([0, 1]).
        //        This value is full body mode only.
        //        **The use of this value is not recommended beacuse in development.**
        //     w: The score of whether the landmark position is visible ([0, 1]).
        
        //     33 index data is the score whether human pose is visible ([0, 1]).
        //     This data is (score, 0, 0, 0).
        //     */

        //     // TODO: add control
        //     Debug.LogFormat("{0}: {1}", i, detecter.GetPoseLandmark(i));
        // }
        // Debug.Log("---");
    }

    void OnRenderObject(){
    //     var w = inputImageUI.rectTransform.rect.width;
    //     var h = inputImageUI.rectTransform.rect.height;

    //     // Use predicted pose landmark results on the ComputeBuffer (GPU) memory.
    //     material.SetBuffer("_vertices", detecter.outputBuffer);
    //     // Set pose landmark counts.
    //     material.SetInt("_keypointCount", detecter.vertexCount);
    //     material.SetFloat("_humanExistThreshold", humanExistThreshold);
    //     material.SetVector("_uiScale", new Vector2(w, h));
    //     material.SetVectorArray("_linePair", linePair);

    //     // Draw 35 body topology lines.
    //     material.SetPass(0);
    //     Graphics.DrawProceduralNow(MeshTopology.Triangles, 6, BODY_LINE_NUM);

    //     // Draw 33 landmark points.
    //   thjrgffffffz   material.SetPass(1);
    //     Graphics.DrawProceduralNow(MeshTopology.Triangles, 6, detecter.vertexCount);
    }

    public void OnApplicationQuit(){
        // Must call Dispose method when no longer in use.
        detecter.Dispose();
    }
}
