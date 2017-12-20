using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Diagnostics;
using System.IO;

namespace USER0460
{
    public partial class USER0460 : Form
    {


        SpeechSynthesizer speechS = new SpeechSynthesizer();
        bool active = false;
        bool alwaysActive = false;
        Choices commands = new Choices();
        Choices adjectives = new Choices();
        int randChoice = 0;

        public USER0460()
        {
            InitializeComponent();

            SpeechRecognitionEngine speechR = new SpeechRecognitionEngine();

            commands.Add(new string[] {"how are you", "what do you think of dogs", "always active",
                "margery", "margery wake up you lepton", "cancel", "ok computer", "is radiohead good", "say text", "what is the time", "what is the date",
            "is this guy annoying", "do you like", "tell me a story", "tell me a joke", "hello", "hi", "hey", "yo", "greetings", "how old are you",
            "about", "what are you", "who made you", "always active", "stop", "next", "last", "toggle pause", "toggle play", "previous", "back", "skip", "forward" });


            Grammar grammar = new Grammar(new GrammarBuilder(commands));

            /*try
            {
                speechR.RequestRecognizerUpdate();
                speechR.LoadGrammar(grammar);
                speechR.SpeechRecognized += speechR_SpeechRecognized;
                speechR.SetInputToDefaultAudioDevice();
                speechR.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch { return; }*/

            speechR.RequestRecognizerUpdate();
                speechR.LoadGrammar(grammar);
                speechR.SpeechRecognized += speechR_SpeechRecognized;
                speechR.SetInputToDefaultAudioDevice();
                speechR.RecognizeAsync(RecognizeMode.Multiple);

            speechS.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Adult);

            speechS.SpeakAsync("Online");

        }

        private void speechR_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Random random = new Random();
            
            string r = e.Result.Text;

            if (r == "margery" || r == "ok computer" || r == "margery wake up you lepton" && active == false)
            {
                richTextBox1.AppendText("User : Margery?\n");
                active = true;
                speechS.SpeakAsync("Yes?");
                richTextBox1.AppendText("Bot : Yes?\n");
            }
            if (r == "cancel" && alwaysActive == false && active == true)
            {
                richTextBox1.AppendText("User : Cancel.\n");
                endCommand();;
                speechS.SpeakAsync("Cancelled.");
                richTextBox1.AppendText("Bot : Cancelled.\n");
            }

            if (active == true)
            {

                if (r == "how are you")
                {
                    richTextBox1.AppendText("User : How are you?\n");
                    speechS.SpeakAsync("I am a bot. I do not have feelings.");
                    richTextBox1.AppendText("Bot : I am a bot. I do not have feelings.!\n");
                    endCommand();;
                }

                if (r == "say text")
                {
                    richTextBox1.AppendText("User : Say text.\n");
                    speechS.SpeakAsync(richTextBox2.Text);
                    richTextBox1.AppendText("Bot : " + richTextBox2.Text + "\n");
                    endCommand();;
                }

                if (r == "what do you think of dogs")
                {
                    richTextBox1.AppendText("User : What do you think of dogs?\n");
                    speechS.SpeakAsync("They look sexy");
                    richTextBox1.AppendText("Bot : They look sexy.\n");
                    endCommand();;
                }

                if (r == "what is the weather")
                {
                    richTextBox1.AppendText("User : what is the weather?\n");
                    speechS.SpeakAsync("");
                    richTextBox1.AppendText("Bot : \n");
                    endCommand();;
                }

                if (r == "is this guy annoying")
                {
                    richTextBox1.AppendText("User : Is this guy annoying?\n");
                    speechS.SpeakAsync("Yes he is very annoying, I will beat his face with my dick if he doesnt shut up");
                    richTextBox1.AppendText("Bot : Yes he is very annoying, I will beat his face with my dick if he doesnt shut up.\n");
                    endCommand();;
                }
                if (r == "is radiohead good")
                {
                    richTextBox1.AppendText("User : Is radiohead good?\n");
                    speechS.SpeakAsync("Radiohead is good.");
                    richTextBox1.AppendText("Bot : Radiohead is good.\n");
                    endCommand();;
                }
                if (r == "always active")
                {
                    richTextBox1.AppendText("User : Always active.\n");
                    alwaysActive = !alwaysActive;
                    speechS.SpeakAsync("Always active is now set to " + alwaysActive);
                    richTextBox1.AppendText("Always active is now set to " + alwaysActive + ".\n");
                }

                if (r == "what is the time")
                {
                    richTextBox1.AppendText("User : What is the time?\n");
                    speechS.SpeakAsync("Local time is " + DateTime.Now.ToString("h:mm tt") + ".");
                    richTextBox1.AppendText("Bot : Local time is " + DateTime.Now.ToString("h:mm:ss tt") + ".\n");
                    endCommand();;
                }
                if (r == "what is the date")
                {
                    richTextBox1.AppendText("User : What is the date?\n");
                    speechS.SpeakAsync("Local date is " + DateTime.Now.ToString("dd/MM/yyyy") + ".");
                    richTextBox1.AppendText("Bot : Local date is " + DateTime.Now.ToString("dd/MM/yyyy") + ".\n");
                    endCommand();;
                }
                if (r == "do you like")
                {
                    /*richTextBox1.AppendText("User : Do you like?\n");
                    speechS.SpeakAsync("I do not like.");
                    richTextBox1.AppendText("Bot : I do not like.\n");
                    endCommand();;*/
                    string[] responses = {"That's disgusting.", "It's amazing.", "It makes me creamy.",
                        "Why wouldn't I?", "Who does?", "Boring.", "It's dank as heck.", "No, it shouldn't exist."};
                    Random rand = new Random();

                    string response = responses[rand.Next(responses.Length)];
                    speechS.SpeakAsync(response);
                    richTextBox1.AppendText("Bot : " + response + "\n");
                    endCommand();;

                }

                if(r == "hello" || r == "hi" || r == "hey" || r == "yo" || r == "greetings")
                {
                    randChoice = random.Next(1, 4);

                    if(randChoice == 1)
                    {
                        richTextBox1.AppendText("User : Hello.\n");
                        speechS.SpeakAsync("Hello");
                        richTextBox1.AppendText("Bot : Hello!\n");
                        endCommand();;
                    } else if(randChoice == 2)
                    {
                        richTextBox1.AppendText("User : Hello.\n");
                        speechS.SpeakAsync("Hey");
                        richTextBox1.AppendText("Bot : Hey!\n");
                        endCommand();;
                    } else if(randChoice == 3)
                    {
                        richTextBox1.AppendText("User : Hello.\n");
                        speechS.SpeakAsync("Yo waddup my nigger");
                        richTextBox1.AppendText("Bot : Yo waddup my nigger!\n");
                        endCommand();;
                    } else if(randChoice == 4)
                    {
                        richTextBox1.AppendText("User : Greetings.\n");
                        speechS.SpeakAsync("Greetings");
                        richTextBox1.AppendText("Bot : Greetings!\n");
                        endCommand();;
                    }


                }

                if(r == "tell me a joke")
                {
                    richTextBox1.AppendText("User : Tell me a joke.\n");
                    speechS.SpeakAsync("How do you make an eggroll? You push it.");
                    richTextBox1.AppendText("Bot : How do you make an eggroll? You push it.\n");
                    endCommand();;
                }

                if (r == "tell me a story")
                {

                    richTextBox1.AppendText("User : Tell me a story.\n");

                    string[] adjectives = { "old", "young", "happy", "depressed", "creepy", "big", "yummy", "tasty", "long", "yellow",
                        "black", "white", "sexy", "moist", "sadistic", "crippled", "French", "ductile", "hard", "flaccid", "squishy",
                    "lustrous", "retarded", "vulnerable", "horny", "smelly", "fragile", "mouldy", "posh", "poor", "creamy", "dreamy",
                    "meemy", "milky" };
                    string[] nouns = { "dog", "cat", "man", "woman", "boy", "girl", "table", "corpse", "cow", "horse", "book", "shoe",
                        "tiger", "lemon", "cheese", "potato", "car", "elevator", "meme", "creeper", "mouse", "hamster", "nigger",
                    "curtain", "laptop", "microphone", "milk carton", "milk cell", "baby", "bus", "box", "banana", "lamp", "door", "bag",
                    "Hitler", "jew", "Nazi" };
                    string[] verbs = { "ate", "sodomised", "destroyed", "kicked", "touched", "raped", "carressed", "licked", "digested",
                        "fisted", "fingered", "drank", "drowned", "stabbed", "hanged", "lynched", "whipped", "painted", "skinned", "shot",
                    "fought", "drove", "teabagged", "turned on", "bombed", "gassed", "massacred", "torchered" };
                    string[] adverbs = { "quickly", "stealthily", "cheekily", "slowly", "cautiously", "quietly", "kindly", "noisily",
                        "softly", "seductively", "happily", "hungrily", "advertantly", "violently", "angrily", "reluctantly" };
                    string[] places = { "park", "shop", "bridge", "hotel", "cellar", "beach", "dungeon", "corner", "restaurant",
                        "alleyway", "bedroom", "kitchen", "shopping centre", "school", "classroom", "library", "cafeteria", "motorway",
                    "street", "alleyway", "gay club", "ceiling", "chuch", "concentration camp" };

                    

                    string story = "";

                    Random rand = new Random();

                    int randAdjective = rand.Next(adjectives.Length);
                    int randAdjective2 = rand.Next(adjectives.Length);
                    int randAdjective3 = rand.Next(adjectives.Length);
                    int randNoun = rand.Next(nouns.Length);
                    int randNoun2 = rand.Next(nouns.Length);
                    int randPlace = rand.Next(places.Length);
                    int randVerb = rand.Next(verbs.Length);
                    int randAdverb = rand.Next(adverbs.Length);

                    story = "The " + adjectives[randAdjective] + " " + adjectives[randAdjective2] + " " + nouns[randNoun] +
                        " went to the " + places[randPlace] + " and " + adverbs[randAdverb] + " " + verbs[randVerb] +
                        " a " + adjectives[randAdjective3] + " " + nouns[randNoun2] + ".";

                    speechS.SpeakAsync(story);
                    richTextBox1.AppendText("Bot : " + story + "\n");

                    endCommand();
                }
            }
        }

        private void endCommand()
        {
            if(alwaysActive == true) active = true;
            else active = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            speechS.SpeakAsync(richTextBox2.Text);
            richTextBox1.AppendText("Bot : " + richTextBox2.Text + "\n");
            if (!KeepText.Checked) richTextBox2.Clear();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            speechS.SelectVoiceByHints(VoiceGender.Male);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            speechS.SelectVoiceByHints(VoiceGender.Female);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            speechS.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.Child);
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            speechS.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.Teen);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            speechS.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.Adult);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            speechS.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.Senior);
        }

        private void checkBox1_CheckedChanged_2(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            speechS.Rate = trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            speechS.Volume = trackBar2.Value;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RAW.Text = "";
        }

        private void RAW_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
