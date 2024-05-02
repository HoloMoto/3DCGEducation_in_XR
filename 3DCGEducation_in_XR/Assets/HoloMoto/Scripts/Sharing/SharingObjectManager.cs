using UnityEngine;

namespace HoloMoto.Manager
{
    public class SharingObjectManager : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            // �^�b�`���������ꍇ
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // �^�b�`�������W���擾
                Vector2 touchPosition = Input.GetTouch(0).position;

                // �^�b�`�������W����Ray���΂�
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                RaycastHit hit;

                // Ray�������ɓ��������ꍇ
                if (Physics.Raycast(ray, out hit))
                {
                    // ���������I�u�W�F�N�g�����������ꍇ�A���g�̃I�u�W�F�N�g�����̍��W�Ɉړ�
                    if (hit.transform.tag == "floor")
                    {
                        transform.position = hit.point;
                    }
                }
            }
        }
    }
}
