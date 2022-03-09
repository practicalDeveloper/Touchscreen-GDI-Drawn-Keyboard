using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;
using Example.Properties;
using Keyboard;

namespace Example
{
    public enum Languages
    {
        English = 1,
        Russian = 2,
        Greerk = 3
    }


    public partial class KeyboradExampleForm : Form
    {
        const uint WS_EX_NOACTIVATE = 0x08000000;
        const uint WS_EX_TOPMOST = 0x00000008;
        private const string en = "En";
        private const string gr = "Gr";
        private const string ru = "Ru";
        private int langType = 1;
        private string layoutDefaultFilePath = Application.StartupPath + "\\KeyboardLayouts";

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams baseParams = base.CreateParams;
                baseParams.ExStyle |= (int)(WS_EX_NOACTIVATE | WS_EX_TOPMOST);
                return baseParams;
            }
        }

        public KeyboradExampleForm()
        {
            InitializeComponent();
        }


        private void virtualKeyboard1_KeyboardButtonPressed(string command, KeyboardButtonEventArgs e)
        {
            if (command == KeyboardKeyConstants.LanguageSelection)
            {
                switch (virtualKeyboard1.LanguageButtonTopText)
                {
                    case en:
                        SetLangButton(ru, gr, Resources.Flag_Ru);
                        langType = (int)Languages.Russian;
                        SetRusLayout();
                    break;
                    case ru:
                        SetLangButton(gr, en, Resources.Flag_Greece);
                        langType = (int)Languages.Greerk;
                        SetGreekLayout();
                        break;
                    case gr:
                        SetLangButton(en, ru, Resources.Flag_UK);
                        langType = (int)Languages.English;
                        ResetLayout();
                        break;

                }
            }
            else
            {
                    switch (langType)
                    {
                        case (int) Languages.English:
                            ResetLayout();
                            break;
                        case (int) Languages.Russian:
                            SetRusLayout();
                            break;
                        case (int)Languages.Greerk:
                            SetGreekLayout();
                            break;
                        default:

                            break;
                    }
                }


        }

        private void SetRusLayout()
        {
            var xmlPath = layoutDefaultFilePath + "\\KeyboardLayout_RusLower.XML";

            if (virtualKeyboard1.ShiftState && virtualKeyboard1.CapsLockState)
            {
                xmlPath = layoutDefaultFilePath + "\\KeyboardLayout_RusLower.XML";
            }
            else if (virtualKeyboard1.ShiftState || virtualKeyboard1.CapsLockState)
            {
                xmlPath = layoutDefaultFilePath + "\\KeyboardLayout_Rus.XML";
            }
            else
            {
                xmlPath = layoutDefaultFilePath + "\\KeyboardLayout_RusLower.XML";
            }


            XDocument xmlDocument = XDocument.Load(xmlPath);
            var firstRow = LoadButtonsRow(xmlDocument, 1);
            var secondRow = LoadButtonsRow(xmlDocument, 2);
            var thirdRow = LoadButtonsRow(xmlDocument, 3);
            var fourthRow = LoadButtonsRow(xmlDocument, 4);
            var fifthRow = LoadButtonsRow(xmlDocument, 5);

            virtualKeyboard1.FirstRowCustomButtons = firstRow;
            virtualKeyboard1.SecondRowCustomButtons = secondRow;
            virtualKeyboard1.ThirdRowCustomButtons = thirdRow;
            virtualKeyboard1.FourthRowCustomButtons = fourthRow;
            virtualKeyboard1.FifthRowCustomButtons = fifthRow;
        }

        private void SetGreekLayout()
        {
            var xmlPath = layoutDefaultFilePath + "\\KeyboardLayout_GrLower.XML";

            if (virtualKeyboard1.AltGrState)
            {
                xmlPath = layoutDefaultFilePath + "\\KeyboardLayout_GrAltGr.XML";
            }
            else
            {
                if (virtualKeyboard1.ShiftState && virtualKeyboard1.CapsLockState)
                {
                    xmlPath = layoutDefaultFilePath + "\\KeyboardLayout_GrLower.XML";
                }
                else if (virtualKeyboard1.ShiftState || virtualKeyboard1.CapsLockState)
                {
                    xmlPath = layoutDefaultFilePath + "\\KeyboardLayout_Gr.XML";
                }
                else
                {
                    xmlPath = layoutDefaultFilePath + "\\KeyboardLayout_GrLower.XML";
                }
            }


            XDocument xmlDocument = XDocument.Load(xmlPath);
            var firstRow = LoadButtonsRow(xmlDocument, 1);
            var secondRow = LoadButtonsRow(xmlDocument, 2);
            var thirdRow = LoadButtonsRow(xmlDocument, 3);
            var fourthRow = LoadButtonsRow(xmlDocument, 4);
            var fifthRow = LoadButtonsRow(xmlDocument, 5);

            virtualKeyboard1.FirstRowCustomButtons = firstRow;
            virtualKeyboard1.SecondRowCustomButtons = secondRow;
            virtualKeyboard1.ThirdRowCustomButtons = thirdRow;
            virtualKeyboard1.FourthRowCustomButtons = fourthRow;
            virtualKeyboard1.FifthRowCustomButtons = fifthRow;
        }


        private void SetLangButton(string topText, string bottomText, Image img)
        {
            virtualKeyboard1.LanguageButtonTopText = topText;
            virtualKeyboard1.LanguageButtonBottomText = bottomText;
            virtualKeyboard1.LanguageButtonImage = img;
        }

        private void btn_RunWordPad_Click(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.Start("wordpad.exe");
        }

        private void btn_RunNotepad_Click(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe");
        }

        private void KeyboradExampleForm_Load(object sender, System.EventArgs e)
        {
            virtualKeyboard1.ShowLanguageButton = true;
            SetLangButton(en, ru, Resources.Flag_UK);
            langType = (int)Languages.English;
            ResetLayout();
        }


        private ButtonsCollection LoadButtonsRow(XDocument xmlDocument, int rowNumber)
        {
            var buttonsCollection = new ButtonsCollection();

            // Loads buttons text by XML row number
            var node = xmlDocument.XPathSelectElement(string.Format("//Row[RowNumber = {0}]/Buttons", rowNumber));
            var dataText = (from contact in node.Descendants("Text")
                            select new
                            {
                                TopText = contact.Element("UpText").Value,
                                BottomText = contact.Element("BottomText").Value
                            }).ToList();

            foreach (var text in dataText)
            {
                var kbButton = new VirtualKbButton();
                kbButton.TopText = text.TopText;
                kbButton.BottomText = text.BottomText;
                buttonsCollection.Add(kbButton);
            }

            return buttonsCollection;
        }

        private void ResetLayout()
        {
            StateHelpers.ApplyState(virtualKeyboard1);
        }
    }
}


