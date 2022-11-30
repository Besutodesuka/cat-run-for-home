import cv2
from time import time
camera_video = cv2.VideoCapture(0, cv2.CAP_DSHOW)
camera_video.set(3, 1280)
camera_video.set(4, 960)
cv2.namedWindow('PuddingMaprawnon2', cv2.WINDOW_NORMAL);
time1 = 0
while True:
    ok, frame = camera_video.read()

    # Set the time for this frame to the current time.
    time2 = time()

    # Check if the difference between the previous and this frame time > 0 to avoid division by zero.
    if (time2 - time1) > 0:
        # Calculate the number of frames per second.
        frames_per_second = 1.0 / (time2 - time1)

        # Write the calculated number of frames per second on the frame.
        cv2.putText(frame, 'FPS: {}'.format(int(frames_per_second)), (10, 30), cv2.FONT_HERSHEY_PLAIN, 2, (0, 255, 0),
                    3)
    cv2.imshow('PuddingMaprawnon2', frame)
    # Update the previous frame time to this frame time.
    # As this frame will become previous frame in next iteration.
    time1 = time2
    k = cv2.waitKey(1) & 0xFF