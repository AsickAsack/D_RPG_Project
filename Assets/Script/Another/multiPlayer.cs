using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;


public class multiPlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    public Pivot pivot;
    public Transform mycam;
    private GameObject realcam;
    public Transform Pivot;
    public UltimateJoystick ultimateJoystick;
    public static bool JoysticState = false;
    Vector3 Dir = Vector3.zero;
    Quaternion newRotation = Quaternion.identity;
    public TMPro.TMP_Text Myname;
    public PhotonView PV;
    public PhotonView PV1;
    public GameObject Niki;
    public Animator myAnim;
    private Button SkillButton = null;
    private Button AttackButton = null;
    private Button RollButton = null;
    public GameObject[] HP_Bar = null;
    public float RollSpeed=0.01f;
    public Transform RightHand;
    public GameObject ResultCanvas;
    public Text ResultTx;
    public Text WinnerNameTx;
    private string WinnerName;
    


    [Header("플레이어 스텟")]
    public float _HP = 100.0f;
    public float Damage = 10.0f;

    public float HP
    {
        get => _HP;

        set
        {
            _HP = value;

            if (_HP <= 0.0f)
            {
                _HP = 0.0f;
                myAnim.SetTrigger("Die");
                SkillButton.interactable = false;
                AttackButton.interactable = false;
                RollButton.interactable = false;
                if(PV1.IsMine)
                {
                    ResultTx.text = "패배 !";
                    PV1.RPC("openResult", RpcTarget.All);

                   // WinnerName = PhotonNetwork.NickName;
                    
                   
                }
                
               // WinnerNameTx.text = PhotonNetwork == WinnerName? ;
               
                // 이동막기
                // 승리자/패배자 띄우기
                // 메인으로 돌아가기
            }
        }
    }

    [PunRPC]
    public void openResult()
    {
        ResultCanvas.GetComponent<Canvas>().enabled = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        ultimateJoystick = FindObjectOfType<UltimateJoystick>();
        if (PV.IsMine)
        {
            realcam = GameObject.Find("Camera");
            realcam.transform.parent = Pivot;
            realcam.transform.localPosition = mycam.localPosition;
            realcam.transform.localRotation = mycam.localRotation;


        }


        SkillButton = GameObject.Find("Skill").GetComponent<Button>();
        AttackButton = GameObject.Find("Attack").GetComponent<Button>();
        RollButton = GameObject.Find("Roll").GetComponent<Button>();

        SkillButton.onClick.AddListener(() => myAnim.SetTrigger("Skill"));
        AttackButton.onClick.AddListener(() => myAnim.SetTrigger("Attack"));
        RollButton.onClick.AddListener(() => {


            myAnim.SetTrigger("Roll");

            StartCoroutine(FakeMove());
        });

        ResultCanvas = GameObject.Find("ResultCanavs");
        ResultTx = ResultCanvas.transform.GetChild(2).GetComponent<Text>();
        WinnerNameTx = ResultCanvas.transform.GetChild(3).GetComponent<Text>();
    }

    IEnumerator FakeMove()
    {
        float movetime = 2.0f;
        while (movetime >= 0.0f)
        {
            movetime -= Time.deltaTime;
            this.transform.position += Niki.transform.forward.normalized * RollSpeed;


            yield return null;
        }
    }

    public void hitProcess(float Damage)
    {
        PV1.RPC("networkDamage", RpcTarget.All, Damage);
    }

    [PunRPC]
    public void networkDamage(float Damage)
    {
        myAnim.SetTrigger("hit");
        HP -= Damage;
    }

    public void PunchHit()
    {   
        
        Collider[] myCollider = Physics.OverlapSphere(RightHand.position, 0.125f);
        foreach(Collider col in myCollider)
        {
             col.GetComponent<multiPlayer>()?.hitProcess(20.0f);
             

        }
    }




    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y < 0.0f)
        this.transform.position = new Vector3(-0.7f, 10.6f, -67.1f);
       



        if (Myname != null)
        {
            Myname.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
            Myname.color = PV.IsMine ? Color.green : Color.red;
        }
        HP_Bar[0].transform.rotation = mycam.transform.rotation;
        HP_Bar[1].transform.rotation = mycam.transform.rotation;
        Myname.transform.rotation = mycam.transform.rotation;
        

        JoysticState = ultimateJoystick.GetJoystickState();

        if(PV.IsMine)
        {
            HP_Bar[1].GetComponent<Image>().fillAmount = HP/100.0f;
        }
   



        if (Niki != null && PV.IsMine && PV1.IsMine &&!(myAnim.GetBool("IsAttack")|| myAnim.GetBool("IsRoll")|| myAnim.GetBool("IsSkill")))
        {
            float x = UltimateJoystick.GetHorizontalAxis("myJoystick");
            float y = UltimateJoystick.GetVerticalAxis("myJoystick");

            if (!ultimateJoystick.GetJoystickState())
            {

                x = y = 0.0f;
                myAnim.SetBool("IsRun", false);
            }
            else
            {

                myAnim.SetBool("IsRun", true);
            }

            
            Dir.Set(x, 0, y);
            Dir = Dir.normalized;
            Vector3 moveVector = Dir * 5.0f * Time.deltaTime;

            Quaternion v3Rotation = Quaternion.Euler(0, mycam.rotation.eulerAngles.y, 0);

            moveVector = v3Rotation * moveVector;
           


            this.transform.position += new Vector3(moveVector.x, 0, moveVector.z);


                if (!(x == 0 && y == 0) && Dir != Vector3.zero)
                {
                    newRotation = Quaternion.LookRotation(moveVector);
                    Niki.transform.rotation = Quaternion.Slerp(Niki.transform.rotation, newRotation, 10.0f * Time.deltaTime);
                }
            

        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(HP);
            stream.SendNext(HP_Bar[1].GetComponent<Image>().fillAmount);
            stream.SendNext(WinnerNameTx.text);
        }
        else
        {
            HP = (float)stream.ReceiveNext();
            HP_Bar[1].GetComponent<Image>().fillAmount = (float)stream.ReceiveNext();
            WinnerNameTx.text = (string)stream.ReceiveNext();
        }
        
    }
}
