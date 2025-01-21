using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class StateControl : MonoBehaviour
{
    public AudioClip[] intro;
    public AudioClip[] screw;
    public AudioClip[] forward_reach;
    public AudioClip[] stand_one_leg;
    public AudioClip[] jump_one_leg;
    public AudioClip[] walk_heels;
    public AudioClip[] walk_distal;
    public AudioClip[] ending;

    public GameObject music;

    public AudioSource voice_Lisa;
    public LisaControl motion_Lisa;
    public IK_Hands ik_hands_Lisa;
    public IK_LookAt ik_lookAt;
   

    public enum State
    {
        Intro,
        Screw,
        Forward_reach,
        Stand_one_leg,
        Jump_one_leg,
        Walking_heels,
        Walking_distal,
        Tapping,
        Bishops,
        End
    }

    public State state;
    public int sub_state;

    public Transform[] targets;

    void Start()
    {
        Assert.IsNotNull(voice_Lisa);
        Assert.IsNotNull(motion_Lisa);

        StartCoroutine(Intro());
        //StartCoroutine(Bishops());
    }

    void Update()
    {
    }

 

    private IEnumerator Intro() {

        state = State.Intro;
        sub_state = 0;
        yield return new WaitForSeconds(3f); // wait for fade-in

        voice_Lisa.clip = intro[sub_state]; // 1_Hej, jag heter Lisa...
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = intro[sub_state]; // 2_Idag...
        voice_Lisa.PlayDelayed(1f);
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = intro[sub_state]; // 3_OK ...vi testar
        voice_Lisa.PlayDelayed(1f);
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = intro[sub_state]; // 4_Prova att r�cka upp din h�gra hand...
        voice_Lisa.Play();
        yield return new WaitForSeconds(voice_Lisa.clip.length - 1f);
        motion_Lisa.RightHandUp(); // h�j h�ger hand

        yield return new WaitForSeconds(3f);
        sub_state++;
        voice_Lisa.clip = intro[sub_state]; // 5_Fint!
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = intro[sub_state]; // 6_Prova att r�cka upp din v�nstra hand...
        voice_Lisa.Play();
        yield return new WaitForSeconds(voice_Lisa.clip.length - 1f);
        motion_Lisa.LeftHandUp(); // h�j v�nster hand

        yield return new WaitForSeconds(3f);
        sub_state++;
        voice_Lisa.clip = intro[sub_state]; // 7_Bra!
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = intro[sub_state]; // 8_Nu s�tter vi ig�ng!
        voice_Lisa.PlayDelayed(1f);
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        StartCoroutine(Screw());
        //StartCoroutine(Tapping());
    }

    private IEnumerator Screw() {
        state = State.Screw;
        sub_state = 0;

        voice_Lisa.clip = screw[sub_state]; // Titta p� mig hur jag g�r 
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = screw[sub_state]; // H�ll upp handen s�h�r och skruva fram och tillbaka
        voice_Lisa.Play();
        motion_Lisa.RightHandScrew(); // Skruvr�relse h�ger hand
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = screw[sub_state]; // Ungef�r som om du skulle skruva i en gl�dlampa
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);


        sub_state++;
        voice_Lisa.clip = screw[sub_state]; // Vi b�rjar med h�ger arm
        voice_Lisa.PlayDelayed(1);
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = screw[sub_state]; // Prova g�ra likadant
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        yield return new WaitForSeconds(9f);

        sub_state++;
        voice_Lisa.clip = screw[sub_state]; // Bra!
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = screw[sub_state]; // Nu g�r vi samma sak med v�nster arm
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);
        
        motion_Lisa.LeftHandScrew(); // Skruvr�relse v�nster hand

        yield return new WaitForSeconds(10f);
        sub_state++;
        voice_Lisa.clip = screw[sub_state]; // Bra jobbat!
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        StartCoroutine(ForwardReach());
    }

    private IEnumerator ForwardReach() {
        state = State.Forward_reach;
        sub_state = 0;

        voice_Lisa.clip = forward_reach[sub_state]; // Str�ck fram dina h�nder - s� h�r
        voice_Lisa.PlayDelayed(1f);
        motion_Lisa.ForwardReach(); // str�cker fram h�nder 
        yield return new WaitWhile(() => voice_Lisa.isPlaying);                            
        
        sub_state++;
        voice_Lisa.clip = forward_reach[sub_state]; // vi g�r det tillsammans
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = forward_reach[sub_state]; // Blunda och str�ck ut tungan - tills jag s�ger stopp.
        voice_Lisa.PlayDelayed(2f);
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        yield return new WaitForSeconds(24f);
        sub_state++;
        voice_Lisa.clip = forward_reach[sub_state]; // Stopp
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);
        yield return new WaitForSeconds(2f);

        StartCoroutine(StandOneLeg());
    }

    private IEnumerator StandOneLeg() {
        state = State.Stand_one_leg;
        sub_state = 0;
        voice_Lisa.clip = stand_one_leg[sub_state]; // 1_Jag gillar att st� p� ett ben!
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = stand_one_leg[sub_state]; // 2_G�r du det ocks�? 
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        yield return new WaitForSeconds(1f);
        sub_state++;
        voice_Lisa.clip = stand_one_leg[sub_state]; // 3_OK vi provar att st� p� ett ben i trettio sekunder.
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        yield return new WaitForSeconds(1f);
        sub_state++;
        voice_Lisa.clip = stand_one_leg[sub_state]; // 4_ f�rst med h�ger ben
        voice_Lisa.Play();
        motion_Lisa.OneLegStandingRight();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        yield return new WaitForSeconds(30f);
        motion_Lisa.GoIdle();
        sub_state++;
        voice_Lisa.clip = stand_one_leg[sub_state]; // 7_Fint!
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = stand_one_leg[sub_state]; // 6_nu tar vi v�nster ben 
        voice_Lisa.Play();
        motion_Lisa.OneLegStandingLeft();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        yield return new WaitForSeconds(30f);
        motion_Lisa.GoIdle();
        sub_state++;
        voice_Lisa.clip = stand_one_leg[sub_state]; // 5_Bra!
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        StartCoroutine(JumpOneLeg());
    }

    private IEnumerator JumpOneLeg() {
        state = State.Jump_one_leg;
        sub_state = 0;
        yield return new WaitForSeconds(2f);
        voice_Lisa.clip = jump_one_leg[sub_state]; // 1_Nu ska vi hoppa! 
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = jump_one_leg[sub_state]; // 2_Vi kan hoppa tillsammans
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = jump_one_leg[sub_state]; // 3_Vi b�rjar med h�ger ben
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        motion_Lisa.OneLegJumpingRight();

        sub_state++;
        voice_Lisa.clip = jump_one_leg[sub_state]; //  4_F�rs�k att hoppa p� samma st�lle i en halv minut.
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        yield return new WaitForSeconds(12f);
        sub_state++;
        voice_Lisa.clip = jump_one_leg[sub_state]; //  5_J�ttebra!
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = jump_one_leg[sub_state]; //  6_Och nu tar vi v�nster ben
        voice_Lisa.Play();
        motion_Lisa.OneLegJumpingLeft();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);
        
        yield return new WaitForSeconds(12f);
        sub_state++;
        voice_Lisa.clip = jump_one_leg[sub_state]; //  7_Det h�r var kul!
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        StartCoroutine(WalkHeels());
    }

    private IEnumerator WalkHeels() {
        state = State.Walking_heels;
        sub_state = 0;
        yield return new WaitForSeconds(2f);
        voice_Lisa.clip = walk_heels[sub_state]; //  Nu ska vi g� p� h�larna
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = walk_heels[sub_state]; //  titta p� hur jag g�r...
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        motion_Lisa.WalkHeels();
        yield return new WaitForSeconds(20f);
        sub_state++;
        voice_Lisa.clip = walk_heels[sub_state]; // Vad fint du k�mpar 
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        //yield return new WaitForSeconds(2f);

        StartCoroutine(WalkDistal());
    }

    private IEnumerator WalkDistal() {
        state = State.Walking_distal;
        sub_state = 0;
        yield return new WaitForSeconds(1f);
        voice_Lisa.clip = walk_distal[sub_state]; //  Nu en g�ng till fast p� utsidan av f�tterna
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        motion_Lisa.WalkDistal();
        sub_state++;
        voice_Lisa.clip = walk_distal[sub_state]; //  Titta p� mig vi provar tillsammans
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        yield return new WaitForSeconds(16f);
        motion_Lisa.TurnFront();
        yield return new WaitForSeconds(1f);

        sub_state++;
        voice_Lisa.clip = walk_distal[sub_state]; // Det h�r g�r ju j�ttebra! 
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        StartCoroutine(Tapping());
    }

    private IEnumerator Tapping() {
        state = State.Tapping;
        sub_state = 0;
        yield return new WaitForSeconds(1f);
        motion_Lisa.Tapping();
        yield return new WaitForSeconds(10f);
        motion_Lisa.GoIdle();

        StartCoroutine(Bishops());
    }

    private IEnumerator Bishops() {
        state = State.Bishops;
        sub_state = 0;
        yield return new WaitForSeconds(1f);
        ik_hands_Lisa.ActivateLeftHand();
        ik_hands_Lisa.ActivateRightHand();
        yield return new WaitForSeconds(20f);
        ik_hands_Lisa.DeactivateLeftHand();
        ik_hands_Lisa.DeactivateRightHand();

        StartCoroutine(Ending());
    }


    private IEnumerator Ending() {
        state = State.End;
        sub_state = 0;
        yield return new WaitForSeconds(1f);
        voice_Lisa.clip = ending[sub_state]; //  Nu �r vi klara
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = ending[sub_state]; // Vad bra du har jobbat 
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = ending[sub_state]; //  Hoppas vi ses igen!
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        motion_Lisa.RightHandUp();
       
        sub_state++;
        voice_Lisa.clip = ending[sub_state]; //  Hejd�!
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);
        //yield return new WaitForSeconds(1f);

        GameObject.Find("CenterEyeAnchor").GetComponent<OVRScreenFade>().FadeOut(); // fade out
        yield return new WaitForSeconds(5f);
        
        Application.Quit();
    }
}