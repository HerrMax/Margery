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

        #region
        string[] jokes = { "Tell me a joke", "How do you make an eggroll? You push it." };
        string[] opinions = {"That's disgusting.", "It's amazing.", "It makes me creamy.",
                        "Why wouldn't I?", "Who does?", "Boring.", "It's dank as heck.", "No, it shouldn't exist."};
        string[] greetings = { "Hello!", "Hey!", "Yo waddup my nigger!", "Greetings!", "Yo!", "Hi!", "Oy!", "Howdy!", "Sup!", "Fuck off!" };
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
#endregion

        public USER0460()
        {
            InitializeComponent();

            SpeechRecognitionEngine speechR = new SpeechRecognitionEngine();

            commands.Add(new string[] {"how are you", "what do you think of dogs", "always active",
                "margery", "margery wake up you lepton", "cancel", "ok computer", "is radiohead good", "say text", "what is the time", "what is the date",
            "is this guy annoying", "do you like", "tell me a story", "tell me a joke", "hello", "hi", "hey", "yo", "greetings", "how old are you", "what is the weather",
            "about", "what are you", "who made you", "always active", "stop", "next", "last", "toggle pause", "toggle play", "previous", "back", "skip", "forward" });

            Grammar grammar = new Grammar(new GrammarBuilder(commands));

            speechR.RequestRecognizerUpdate();
            speechR.LoadGrammar(grammar);
            speechR.SpeechRecognized += speechR_SpeechRecognized;
            speechR.SetInputToDefaultAudioDevice();
            speechR.RecognizeAsync(RecognizeMode.Multiple);
            speechS.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Adult);

            speechS.SpeakAsync("Online");
        }

        private void speak(string input, string output)
        {
            richTextBox1.AppendText("User : " + input + "\n");
            richTextBox1.AppendText("Margery : " + output + "\n");
            speechS.Speak(output);

            if (alwaysActive == true) active = true;
            else active = false;
        }

        private void speechR_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Random random = new Random();
            
            string r = e.Result.Text;

            if (r == "margery" || r == "ok computer" || r == "margery wake up you lepton" && active == false)
                speak("Margery?", "Yes?"); active = true;

            if (r == "cancel" && alwaysActive == false && active == true)
                speak("Cancel.", "Cancelled.");

            if (active == true)
            {

                if (r == "how are you")
                    speak("How are you?", "I am a bot. I do not have feelings.");

                if (r == "say text")
                    speak("Say text.", richTextBox2.Text);

                if (r == "what do you think of dogs")
                    speak("What do you think of dogs?", "They look sexy.");

                if (r == "what is the weather")
                    speak("What is the weather?", "Google it or something, I don't fucking know.");

                if (r == "is this guy annoying")
                    speak("Is this guy annoying?", "Yes he is very annoying, I will beat his face with my dick if he doesnt shut up.");

                if (r == "is radiohead good")
                    speak("Is radiohead good?", "Radiohead is good.");

                if (r == "always active")
                    alwaysActive = !alwaysActive; speak("Always active.", "Always active is now set to " + alwaysActive);

                if (r == "what are you?")
                    speak("What are you?", "I am a thot.");

                if (r == "what is the time")
                    speak("What is the time?", "Local time is " + DateTime.Now.ToString("h:mm tt") + ".");

                if (r == "what is the date")
                    speak("What is the date?", "Local date is " + DateTime.Now.ToString("dd/MM/yyyy") + ".");

                if (r == "do you like")
                    speak("Do you like", opinions[random.Next(opinions.Length)]);

                if(r == "hello" || r == "hi" || r == "hey" || r == "yo" || r == "greetings")
                    speak("Hello.", greetings[random.Next(greetings.Length)]);

                if(r == "tell me a joke")
                    speak("Tell me a joke.", jokes[random.Next(jokes.Length)]);

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
                        adjectives[randAdjective3] + " " + nouns[randNoun2] + ".");
                }
            }
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
#endregion
}
