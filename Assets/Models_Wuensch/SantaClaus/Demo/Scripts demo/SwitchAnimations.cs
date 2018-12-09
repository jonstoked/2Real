using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace SantaClaus
{
    public class SwitchAnimations : MonoBehaviour
    {
        //this script controls the switching of animations in the santa Claus demo
        //this script  created by Bilgin Sahin (bilginsahin1979@gmail.com) and used with kind permission.
        //enhanced by Oliver Wuensch

        public Animator santaAnimator;
        public int animationIndex = 0;
        public AudioClip[] audioClips;
        public Text txt_Animations;
        public GameObject santaAnimated;
        public GameObject defaultPositionSanta;
        //maxAniIndex is the highest number of Mechanim Animator Parameter indexAni
        private int maxAniIndex = 42;

        void Start()
        {
           
            //play Audio 0 from Switch Animations audio list Santa Greeting, first Animation plays from mechanim automatically
            PlayAudio();
            UpdateText();
        }

        void Update()
        {
            //interpret Keys
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                PreviousAnimation();
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                NextAnimation();

        }

        public void NextAnimation()
        {
            //triggered from Button,pull animations, loop to first animation in List
            animationIndex++;

            if (animationIndex > maxAniIndex)
                animationIndex = 0;

            ResetSantaPosition();
            PlayAnimation();
            PlayAudio();
            UpdateText();
        }

        public void PreviousAnimation()
        {
            //triggered from Button,pull animations
            animationIndex--;

            if (animationIndex < 0)
                animationIndex = maxAniIndex;

            ResetSantaPosition();
            PlayAnimation();
            PlayAudio();
            UpdateText();
        }

        void ResetSantaPosition()
        {
            //reset Santas position for demo, necessary if rootmotion was applied
            santaAnimated.transform.position = defaultPositionSanta.transform.position;
            santaAnimated.transform.rotation = defaultPositionSanta.transform.rotation;
        }

        void PlayAnimation()
        {
            
            //set mechanim Variable to trigger animation
            santaAnimator.SetInteger("indexAni", animationIndex);

        }

        void PlayAudio()
        {
            //play speech audio for certain animations from the audioClips array
            GetComponent<AudioSource>().Stop();

            switch (animationIndex)
            {
                case 0:
                    GetComponent<AudioSource>().PlayOneShot(audioClips[0]);
                    break;
                case 1:
                    GetComponent<AudioSource>().PlayOneShot(audioClips[1]);
                    break;
                case 2:
                    GetComponent<AudioSource>().PlayOneShot(audioClips[2]);
                    break;
                case 3:
                    GetComponent<AudioSource>().PlayOneShot(audioClips[3]);
                    break;
                case 4:
                    GetComponent<AudioSource>().PlayOneShot(audioClips[4]);
                    break;
                case 34:
                    //CU next year talk
                    GetComponent<AudioSource>().PlayOneShot(audioClips[4]);
                    break;
                case 35:
                    //HaHa
                    GetComponent<AudioSource>().PlayOneShot(audioClips[5]);
                    break;
                case 36:
                    //HoHo
                    GetComponent<AudioSource>().PlayOneShot(audioClips[6]);
                    break;
                case 37:
                    //Hope all wishes come true
                    GetComponent<AudioSource>().PlayOneShot(audioClips[7]);
                    break;
                case 38:
                    //Merry Xmas
                    GetComponent<AudioSource>().PlayOneShot(audioClips[8]);
                    break;
                case 39:
                    //Merry Xmas to everyone
                    GetComponent<AudioSource>().PlayOneShot(audioClips[9]);
                    break;
                case 40:
                    //My name is Santa
                    GetComponent<AudioSource>().PlayOneShot(audioClips[10]);
                    break;
                case 41:
                    //What do you want
                    GetComponent<AudioSource>().PlayOneShot(audioClips[11]);
                    break;
                case 42:
                    //What have you been naughty or nice
                    GetComponent<AudioSource>().PlayOneShot(audioClips[12]);
                    break;
            }
        }

        void UpdateText()
        {
            //apply text to interface, prepare santas Props corresponding to animations
            switch (animationIndex)
            {
                case 0:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Talk New Year";
                    SackVisibleInHand(false);
                    PropsInvisibleInHand();
                    GiftInHand(false);
                    break;
                case 1:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Talk Nice Present";
                    SackVisibleInHand(false);
                    GiftInHand(true);
                    PropsInvisibleInHand();
                    break;
                case 2:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Talk Northpole";
                    SackVisibleInHand(false);
                    PropsInvisibleInHand();
                    GiftInHand(false);
                    break;
                case 3:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Talk What do you want";
                    SackVisibleInHand(false);
                    PropsInvisibleInHand();
                    break;
                case 4:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Run (inPlace)";
                    SackVisibleInHand(false);
                    CandyCaneVisibleInHand(true);

                    break;
                case 5:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Walk (inPlace)";
                    SackVisibleInHand(false);
                    break;
                case 6:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Run with Sack (inPlace)";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;
                case 7:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Walk with Sack (inPlace)";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;
                case 8:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Run (Root)";
                    SackVisibleInHand(false);
                    CandyCaneVisibleInHand(true);
                    break;
                case 9:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Run Turn (Root)";
                    SackVisibleInHand(false);
                    break;
                case 10:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Run with Sack (Root)";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;
                case 11:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Run Turn with Sack (Root)";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;
                case 12:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Walk  (Root)";
                    SackVisibleInHand(false);
                    CandyCaneVisibleInHand(true);
                    break;
                case 13:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Walk Turn (Root)";
                    SackVisibleInHand(false);
                    CandyCaneVisibleInHand(true);
                    break;
                case 14:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Walk with Sack (Root)";
                    SackVisibleInHand(true);
                    break;
                case 15:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Walk Turn with Sack (Root)";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;

                case 16:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Walk to Idle (inPlace)";
                    SackVisibleInHand(true);
                    CaneVisibleInHand(true);
                    break;
                case 17:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Walk to Idle  (Root)";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;
                case 18:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Santa Hit";
                    SackVisibleInHand(false);
                    CaneVisibleInHand(true);
                    break;
                case 19:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Santa Idle";
                    SackVisibleInHand(false);
                    CandyCaneVisibleInHand(true);
                    break;
                case 20:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Santa Idle with Sack";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;
                case 21:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Santa Hit with Sack";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;
                case 22:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Santa Run turn left";
                    SackVisibleInHand(false);
                    CandyCaneVisibleInHand(true);
                    break;
                case 23:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Santa Run turn Left with Sack";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;
                case 24:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Santa Walk Turn Left";
                    SackVisibleInHand(false);
                    CandyCaneVisibleInHand(true);
                    break;
                case 25:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Santa Walk with Sack Turn Left";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;
                case 26:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Santa Jump";
                    SackVisibleInHand(false);
                    CandyCaneVisibleInHand(true);
                    break;
                case 27:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Santa Jump with Sack ";
                    SackVisibleInHand(true);
                    CaneVisibleInHand(true);
                    break;
                case 28:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Santa Die with or without Sack";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;
                case 29:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Hit with Sack";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;
                case 30:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Victory";
                    SackVisibleInHand(false);
                    CandyCaneVisibleInHand(true);
                    break;
                case 31:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Victory with Sack";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;
                case 32:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Slide in Place";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;
                case 33:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "Slide Root";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;
                case 34:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "talk ByeBye";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;
                case 35:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "talk HaHa";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;
                case 36:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "talk HoHo";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;
                case 37:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "talk Wishes come true";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;
                case 38:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "talk Merry Xmas";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;
                case 39:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "talk Merry Xmas everyone";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;
                case 40:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "talk My name is Santa";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;
                case 41:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "talk What do you want";
                    SackVisibleInHand(true);
                    CandyCaneVisibleInHand(true);
                    break;
                case 42:
                    txt_Animations.text = "Animation: " + animationIndex + "\n" + "talk What have you been";
                    SackVisibleInHand(true);
                    CaneVisibleInHand(true);
                    break;

            }


        }
        bool SackVisibleInHand(bool Vis)
        {
            //turn on and off Sack for appropriate animations
            var m_ObjwithSantaConfig = GameObject.FindObjectOfType(typeof(SantaClausConfig)) as SantaClausConfig;
            m_ObjwithSantaConfig.SackVisible = Vis;
            m_ObjwithSantaConfig.SackAttachedToHand = Vis;
            return Vis;
        }
        bool CandyCaneVisibleInHand(bool Vis)
        {
            //turn on and off CandyCane for appropriate animations
            var m_ObjwithSantaConfig = GameObject.FindObjectOfType(typeof(SantaClausConfig)) as SantaClausConfig;
            m_ObjwithSantaConfig.CandyCaneVisible = Vis;
            m_ObjwithSantaConfig.CaneVisible = !Vis;
            m_ObjwithSantaConfig.CandyCaneAttachedToHand = Vis;
            return Vis;
        }
        bool CaneVisibleInHand(bool Vis)
        {
            //turn on and off Cane for appropriate animations
            var m_ObjwithSantaConfig = GameObject.FindObjectOfType(typeof(SantaClausConfig)) as SantaClausConfig;
            m_ObjwithSantaConfig.CaneVisible = Vis;
            m_ObjwithSantaConfig.CandyCaneVisible = !Vis;
            m_ObjwithSantaConfig.CaneAttachedToHand = Vis;
            return Vis;
        }
        void PropsInvisibleInHand()
        {
            //turn off Canes and Sack at once for appropriate animations
            var m_ObjwithSantaConfig = GameObject.FindObjectOfType(typeof(SantaClausConfig)) as SantaClausConfig;
            m_ObjwithSantaConfig.CaneVisible = false;
            m_ObjwithSantaConfig.CandyCaneVisible = false;
            m_ObjwithSantaConfig.SackVisible = false;


        }
        void GiftInHand(bool Vis)
        {
            //turn on and off Gift for appropriate animations
            var m_ObjwithSantaConfig = GameObject.FindObjectOfType(typeof(SantaClausConfig)) as SantaClausConfig;
            if (Vis)
            {
                m_ObjwithSantaConfig.GiftAttachedToHand = true;
                m_ObjwithSantaConfig.SackVisible = false;
            }
            else
                m_ObjwithSantaConfig.GiftAttachedToHand = false;

        }

    }
}

