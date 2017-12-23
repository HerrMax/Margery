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
using USER0460.Properties;

namespace USER0460
{
    public partial class USER0460 : Form
    {

        SpeechSynthesizer speechS = new SpeechSynthesizer();
        bool active = false;
        bool alwaysActive = false;
        Choices commands = new Choices();
        string lastCommand = "";

        //Lists full of responses
        #region
        string[] jokes = File.ReadAllLines(@"C:\Dictionaries\Jokes.txt");
        string[] opinions = File.ReadAllLines(@"C:\Dictionaries\Opinions.txt");
        string[] greetings = File.ReadAllLines(@"C:\Dictionaries\Greetings.txt");
        string[] adjectives = File.ReadAllLines(@"C:\Dictionaries\Adjectives.txt");
        string[] nouns = File.ReadAllLines(@"C:\Dictionaries\Nouns.txt");
        string[] verbs = File.ReadAllLines(@"C:\Dictionaries\PastVerbs.txt");
        string[] adverbs = File.ReadAllLines(@"C:\Dictionaries\Adverbs.txt");
        string[] swears = File.ReadAllLines(@"C:\Dictionaries\Swears.txt");
        string[] places = { "park", "shop", "bridge", "hotel", "cellar", "beach", "dungeon", "corner", "restaurant",
                        "alleyway", "bedroom", "kitchen", "shopping centre", "school", "classroom", "library", "cafeteria", "motorway",
                    "street", "alleyway", "gay club", "ceiling", "chuch", "concentration camp" };
#endregion

        public USER0460()
        {
            InitializeComponent();

            SpeechRecognitionEngine speechR = new SpeechRecognitionEngine();

            commands.Add(new string[] {"how are you", "what do you think of dogs", "always active",
                "margery", "margery wake up you lepton", "cancel", "ok computer", "is radiohead good", "say text", "what is the time", "what is the date",
            "is this guy annoying", "do you like", "tell me a story", "tell me a joke", "hello", "hi", "hey", "yo", "greetings", "how old are you", "what is the weather",
            "about", "what are you", "who made you", "always active", "stop", "shut up", "next", "last", "toggle pause", "toggle play", "previous", "back", "skip", "forward", "sorry",
                "what was that", "i didn't get that", "insult me" });

            Grammar grammar = new Grammar(new GrammarBuilder(commands));

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

            RAW.AppendText(r + "\n");

            if(r == "margery" || r == "ok computer" || r == "margery wake up you lepton" && active == false)
            {
                speak("Margery?", "Yes?", false);
                active = true;
            }

            if(r == "cancel" && alwaysActive == false && active == true)
            {
                speak("Cancel.", "Cancelled.", false);
            }

            if(r == "stop" || r == "shut up")
            {
                speechS.SpeakAsyncCancelAll();
            }
                
            if (active == true)
            {
                if(r == "sorry" || r == "what was that" || r == "i didn't get that")
                    speak("What was that?", @"I said """ + lastCommand + @""" you deaf cunt.", false);
                
                if (r == "how are you")
                    speak("How are you?", "I am a bot. I do not have feelings.", true);

                if (r == "say text")
                    speak("Say text.", SayBox.Text, true);

                if (r == "what do you think of dogs")
                    speak("What do you think of dogs?", "They look sexy.", true);

                if (r == "what is the weather")
                    speak("What is the weather?", "Google it or something, I don't fucking know.", true);

                if (r == "is this guy annoying")
                    speak("Is this guy annoying?", "Yes he is very annoying, I will beat his face with my dick if he doesnt shut up.", true);

                if (r == "is radiohead good")
                    speak("Is radiohead good?", "Radiohead is good.", true);

                if (r == "always active")
                {
                    alwaysActive = !alwaysActive;
                    speak("Always active.", "Always active is now set to " + alwaysActive, true);
                } 

                if (r == "what are you")
                    speak("What are you?", "I am a thot.", true);

                if (r == "what is the time")
                    speak("What is the time?", "Local time is " + DateTime.Now.ToString("h:mm tt") + ".", true);

                if (r == "what is the date")
                    speak("What is the date?", "Local date is " + DateTime.Now.ToString("dd/MM/yyyy") + ".", true);

                if (r == "do you like")
                    speak("Do you like", opinions[random.Next(opinions.Length)], true);

                //if(r == "hello" || r == "hi" || r == "hey" || r == "yo" || r == "greetings")
                //speak("Hello.", greetings[random.Next(greetings.Length)], true);

                if (r == "tell me a joke")
                    speak("Tell me a joke.", jokes[random.Next(jokes.Length)], true);

                if(r == "insult me")
                    speak("Insult me.", "You " + swears[random.Next(swears.Length)] + " " + swears[random.Next(swears.Length)] + " " + swears[random.Next(swears.Length)] + ".", true);

                if (r == "tell me a story")
                {
                    int randAdjective = random.Next(adjectives.Length);
                    int randAdjective2 = random.Next(adjectives.Length);
                    int randAdjective3 = random.Next(adjectives.Length);
                    int randNoun = random.Next(nouns.Length);
                    int randNoun2 = random.Next(nouns.Length);
                    int randPlace = random.Next(places.Length);
                    int randVerb = random.Next(verbs.Length);
                    int randAdverb = random.Next(adverbs.Length);

                    speak("Tell me a story", "The " + adjectives[randAdjective] + " " + adjectives[randAdjective2] + " " + nouns[randNoun] +
                        " went to the " + places[randPlace] + " and " + adverbs[randAdverb] + " " + verbs[randVerb] + " a " + 
                        adjectives[randAdjective3] + " " + nouns[randNoun2] + ".", true);
                }
            }
        }

        private void speak(string input, string output, bool keepCommand)
        {
            Console.AppendText("User : " + input + "\n");
            Console.AppendText("Margery : " + output + "\n");
            speechS.SpeakAsync(output);

            if (alwaysActive == true) active = true;
            else active = false;
            if(keepCommand == true) lastCommand = output;
        }

        //Contains negligible UI stuff
        #region
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
            speechS.SpeakAsync(SayBox.Text);
            Console.AppendText("Margery : " + SayBox.Text + "\n");
            if (!KeepText.Checked) SayBox.Clear();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.Clear();
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
#endregion
}
