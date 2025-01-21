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
        voice_Lisa.clip = intro[sub_state]; // 4_Prova att räcka upp din högra hand...
        voice_Lisa.Play();
        yield return new WaitForSeconds(voice_Lisa.clip.length - 1f);
        motion_Lisa.RightHandUp(); // höj höger hand

        yield return new WaitForSeconds(3f);
        sub_state++;
        voice_Lisa.clip = intro[sub_state]; // 5_Fint!
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = intro[sub_state]; // 6_Prova att räcka upp din vänstra hand...
        voice_Lisa.Play();
        yield return new WaitForSeconds(voice_Lisa.clip.length - 1f);
        motion_Lisa.LeftHandUp(); // höj vänster hand

        yield return new WaitForSeconds(3f);
        sub_state++;
        voice_Lisa.clip = intro[sub_state]; // 7_Bra!
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = intro[sub_state]; // 8_Nu sätter vi igång!
        voice_Lisa.PlayDelayed(1f);
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        StartCoroutine(Screw());
        //StartCoroutine(Tapping());
    }

    private IEnumerator Screw() {
        state = State.Screw;
        sub_state = 0;

        voice_Lisa.clip = screw[sub_state]; // Titta på mig hur jag gör 
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = screw[sub_state]; // Håll upp handen såhär och skruva fram och tillbaka
        voice_Lisa.Play();
        motion_Lisa.RightHandScrew(); // Skruvrörelse höger hand
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = screw[sub_state]; // Ungefär som om du skulle skruva i en glödlampa
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);


        sub_state++;
        voice_Lisa.clip = screw[sub_state]; // Vi börjar med höger arm
        voice_Lisa.PlayDelayed(1);
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = screw[sub_state]; // Prova göra likadant
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        yield return new WaitForSeconds(9f);

        sub_state++;
        voice_Lisa.clip = screw[sub_state]; // Bra!
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = screw[sub_state]; // Nu gör vi samma sak med vänster arm
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);
        
        motion_Lisa.LeftHandScrew(); // Skruvrörelse vänster hand

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

        voice_Lisa.clip = forward_reach[sub_state]; // Sträck fram dina händer - så här
        voice_Lisa.PlayDelayed(1f);
        motion_Lisa.ForwardReach(); // sträcker fram händer 
        yield return new WaitWhile(() => voice_Lisa.isPlaying);                            
        
        sub_state++;
        voice_Lisa.clip = forward_reach[sub_state]; // vi gör det tillsammans
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = forward_reach[sub_state]; // Blunda och sträck ut tungan - tills jag säger stopp.
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
        voice_Lisa.clip = stand_one_leg[sub_state]; // 1_Jag gillar att stå på ett ben!
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = stand_one_leg[sub_state]; // 2_Gör du det också? 
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        yield return new WaitForSeconds(1f);
        sub_state++;
        voice_Lisa.clip = stand_one_leg[sub_state]; // 3_OK vi provar att stå på ett ben i trettio sekunder.
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        yield return new WaitForSeconds(1f);
        sub_state++;
        voice_Lisa.clip = stand_one_leg[sub_state]; // 4_ först med höger ben
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
        voice_Lisa.clip = stand_one_leg[sub_state]; // 6_nu tar vi vänster ben 
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
        voice_Lisa.clip = jump_one_leg[sub_state]; // 3_Vi börjar med höger ben
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        motion_Lisa.OneLegJumpingRight();

        sub_state++;
        voice_Lisa.clip = jump_one_leg[sub_state]; //  4_Försök att hoppa på samma ställe i en halv minut.
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        yield return new WaitForSeconds(12f);
        sub_state++;
        voice_Lisa.clip = jump_one_leg[sub_state]; //  5_Jättebra!
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = jump_one_leg[sub_state]; //  6_Och nu tar vi vänster ben
        voice_Lisa.Play();
        motion_Lisa.OneLegJumpingLeft();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);
        
        yield return new WaitForSeconds(12f);
        sub_state++;
        voice_Lisa.clip = jump_one_leg[sub_state]; //  7_Det här var kul!
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        StartCoroutine(WalkHeels());
    }

    private IEnumerator WalkHeels() {
        state = State.Walking_heels;
        sub_state = 0;
        yield return new WaitForSeconds(2f);
        voice_Lisa.clip = walk_heels[sub_state]; //  Nu ska vi gå på hälarna
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        sub_state++;
        voice_Lisa.clip = walk_heels[sub_state]; //  titta på hur jag gör...
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        motion_Lisa.WalkHeels();
        yield return new WaitForSeconds(20f);
        sub_state++;
        voice_Lisa.clip = walk_heels[sub_state]; // Vad fint du kämpar 
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        //yield return new WaitForSeconds(2f);

        StartCoroutine(WalkDistal());
    }

    private IEnumerator WalkDistal() {
        state = State.Walking_distal;
        sub_state = 0;
        yield return new WaitForSeconds(1f);
        voice_Lisa.clip = walk_distal[sub_state]; //  Nu en gång till fast på utsidan av fötterna
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        motion_Lisa.WalkDistal();
        sub_state++;
        voice_Lisa.clip = walk_distal[sub_state]; //  Titta på mig vi provar tillsammans
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);

        yield return new WaitForSeconds(16f);
        motion_Lisa.TurnFront();
        yield return new WaitForSeconds(1f);

        sub_state++;
        voice_Lisa.clip = walk_distal[sub_state]; // Det här går ju jättebra! 
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
        voice_Lisa.clip = ending[sub_state]; //  Nu är vi klara
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
        voice_Lisa.clip = ending[sub_state]; //  Hejdå!
        voice_Lisa.Play();
        yield return new WaitWhile(() => voice_Lisa.isPlaying);
        //yield return new WaitForSeconds(1f);

        GameObject.Find("CenterEyeAnchor").GetComponent<OVRScreenFade>().FadeOut(); // fade out
        yield return new WaitForSeconds(5f);
        
        Application.Quit();
    }
}