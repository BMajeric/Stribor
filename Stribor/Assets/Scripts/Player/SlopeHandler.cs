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

    private Rigidbody rigid;

    #endregion

    private void Awake() {
        fpc = gameObject.GetComponent<FirstPersonController>();
        rigid = this.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    private void FixedUpdate() {

        if (OnSteepSlope()) {
            fpc.playerCanMove[0] = false;
            fpc.enableJump = false;
        } else {
            fpc.playerCanMove[0] = true;
            fpc.enableJump = true;
        }
        
    }

    private bool OnSteepSlope() {

        Vector3 smjervektor = rigid.velocity + Vector3.down;

        if (Physics.Raycast(ociLevel.position, smjervektor, out hitSlope, 2f)) {

            float slopeAngle = Vector3.Angle(Vector3.up, hitSlope.normal);

            Debug.DrawRay(ociLevel.position, smjervektor, Color.red, 3);

            return slopeAngle > maxSlopeAngle;
        }
        return false;

    }

}
