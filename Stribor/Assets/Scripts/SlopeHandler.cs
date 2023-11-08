using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeHandler : MonoBehaviour
{
    #region Slope Handling
    public float maxSlopeAngle;
    private RaycastHit hitSlope;

    public Transform ociLevel;

    private FirstPersonController fpc;

    #endregion

    private void Awake() {
        fpc = gameObject.GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    private void FixedUpdate() {

        if (OnSteepSlope()) {
            fpc.playerCanMove = false;
            fpc.enableJump = false;
        } else {
            fpc.playerCanMove = true;
            fpc.enableJump = true;
        }
        
    }

    private bool OnSteepSlope() {

        if (Physics.Raycast(ociLevel.position, Vector3.down, out hitSlope, 2f)) {

            float slopeAngle = Vector3.Angle(Vector3.up, hitSlope.normal);

            Debug.DrawRay(ociLevel.position, Vector3.down, Color.red, 2);

            return slopeAngle > maxSlopeAngle;
        }
        return false;

    }

}
