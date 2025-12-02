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

namespace AI
{
    public partial class Form1 : Form
    {

        SpeechSynthesizer s = new SpeechSynthesizer();
        Boolean wake = true;
        Choices list = new Choices();

        public Boolean search = false;

        public Form1()
        {

            SpeechRecognitionEngine rec = new SpeechRecognitionEngine();

            list.Add(File.ReadAllLines(@"C:\Users\Lenovo\Documents\AI\AI\AI Commands\Cmds.txt"));

            Grammar gr = new Grammar(new GrammarBuilder(list));

            try
            {
                rec.RequestRecognizerUpdate();
                rec.LoadGrammar(gr);
                rec.SpeechRecognized += Rec_SpeechRecognized;
                rec.SetInputToDefaultAudioDevice();
                rec.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch { return; }


            s.SelectVoiceByHints(VoiceGender.Male);
            s.Speak("Welcome sir");
            InitializeComponent();
        }
        

        public static void KillProg(String s)
        {
            foreach (var process in Process.GetProcessesByName(s))
            {
                process.Kill();
            }
          /*  System.Diagnostics.Process[] procs = null;

            try
            {
                procs = Process.GetProcessesByName(s);
                Process prog = procs[0];

                if (!prog.HasExited) {
                    prog.Kill();
                }
            }

            finally
            {
                if (procs != null) {
                foreach (Process p in procs) {
                    p.Dispose();
                }
            }

        }
        */
    }

        public void restart()
        {
            Process.Start(@"C:\Users\Lenovo\Documents\AI\AI\AI\bin\Debug\AI.exe");
            Environment.Exit(0);
        }

        public void Say(String h)
        {
            //s.Speak(h);
            s.SpeakAsync(h);
            textbox2.AppendText(h + Environment.NewLine);
        }

        private void Rec_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            String r = e.Result.Text;

            if (r == "wake") { wake = true;
                Say("Waking up sir");
                label3.Text = "Status: Awake";
            }

            if (r == "sleep") { wake = false;
                Say("Going to sleep mode");
                label3.Text = "Status: Sleeping";
            }

            if (search)
            {
                Process.Start("https://www.google.com/search?q="+r);
                search = false;
            }

            if (wake == true && search == false)
            {
                
                if(r == "search for")
                {
                    search = true;
                }

                if(r == "restart" || r == "update")
                {
                    restart();
                }

                if (r == "good morning")
                {
                    Say("good morning sir");

                }

                if (r == "hello")
                {
                    Say("hello sir");

                }

                if (r == "what time is it")
                {
                    Say(DateTime.Now.ToString("h:mm tt") + " sir");
                }

                if (r == "what is today")
                {
                    Say("Its " + DateTime.Now.DayOfWeek + " sir");
                }

                if (r == "how are you")
                {
                    Say("programs were working fine thanks to you sir for modifying them");
                }
                if(r == "open notepad")
                {
                    Process.Start(@"C:\Windows\System32\notepad.exe");
                }
                 if (r == "kill notepad")
                 {
                   KillProg("notepad");
                }
                 if(r == "browse" || r == "search")
                {
                    Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome.exe");
                }
                 if(r == "kill browsing")
                {
                    KillProg("chrome");
                }
                if (r == "minimize")
                {
                    this.WindowState = FormWindowState.Minimized;
                }
                if (r == "maximize")
                {
                    this.WindowState = FormWindowState.Maximized;
                }
                if (r == "normal")
                {
                    this.WindowState = FormWindowState.Normal;
                }
                if(r == "music")
                {
                    Process.Start(@"C:\Users\Lenovo\AppData\Roaming\Spotify\Spotify.exe");
                }
                if(r == "play" || r == "pause")
                {
                    SendKeys.Send(" ");
                }
                if(r == "next song")
                {
                    SendKeys.Send("^{RIGHT}");
                }
            }
            textbox1.AppendText(r + Environment.NewLine);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
