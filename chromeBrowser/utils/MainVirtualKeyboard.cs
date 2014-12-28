using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace chromeBrowser
{
   
    public partial class MainVirtualKeyboard : Form
    {
        
        bool boolCapsLock;
        private ComboBox InputComboBox;
        //private RichTextBox InputText;
        private System.Windows.Controls.TextBox InputText;
        private System.Windows.Controls.PasswordBox passText;
        private int reciever;
        private System.Windows.Controls.TextBox targetControl;


        public MainVirtualKeyboard()
        {
            InitializeComponent();
        }

        public MainVirtualKeyboard(ref RichTextBox Input)
        {
            //reciever = InputControl.richtextbox;
            //this.InputText = Input;
        }
        public MainVirtualKeyboard(ref System.Windows.Controls.TextBox Input)
        {
            //reciever = System.Windows.Controls.TextBox;
            //this.InputText = Input;
        }
        public MainVirtualKeyboard(ref ComboBox Input)
        {
            //reciever = InputControl.combo;
            this.InputComboBox = Input;
        }
        private void ShowTip(object sender)
        {
            toolTip1.Show(((Control)sender).Name, (Control)sender);
        }
        private void ShowTip(object sender,string text)
        {
            toolTip1.Show(text, (Control)sender);
        }
        private void CapsLock_Click(object sender, EventArgs e)
        {
            if (CapsLock.ForeColor == Color.Black)
            {
                boolCapsLock = true;
                CapsLock.ForeColor = Color.Green;
                Q.Text = "Q";
                W.Text = "W";
                E1.Text = "E";
                R.Text = "R";
                T.Text = "T";
                Y.Text = "Y";
                U.Text = "U";
                I.Text = "I";
                O.Text = "O";
                P.Text = "P";
                A.Text = "A";
                S.Text = "S";
                D.Text = "D";
                F.Text = "F";
                G.Text = "G";
                H.Text = "H";
                J.Text = "J";
                K.Text = "K";
                L.Text = "L";
                Z.Text = "Z";
                X.Text = "X";
                C.Text = "C";
                V.Text = "V";
                B.Text = "B";
                N.Text = "N";
                M.Text = "M";
            }
            else
            {
                boolCapsLock = false;
                CapsLock.ForeColor = Color.Black;
                Q.Text = "q";
                W.Text = "w";
                E1.Text = "e";
                R.Text = "r";
                T.Text = "t";
                Y.Text = "y";
                U.Text = "u";
                I.Text = "i";
                O.Text = "o";
                P.Text = "p";
                A.Text = "a";
                S.Text = "s";
                D.Text = "d";
                F.Text = "f";
                G.Text = "g";
                H.Text = "h";
                J.Text = "j";
                K.Text = "k";
                L.Text = "l";
                Z.Text = "z";
                X.Text = "x";
                C.Text = "c";
                V.Text = "v";
                B.Text = "b";
                N.Text = "n";
                M.Text = "m";
            }
        }

        private void Num1_Click(object sender, EventArgs e)
        {
            pressKey(Num1.Text);
        }

        private void pressKey(string resultKey)
        {

            if ((reciever == 1) && (InputText.Text == "User Email") )
            {
                InputText.Text = "";
            }

            if (resultKey == "q" || resultKey == "Q")
            { Q.Focus(); }
            else if (resultKey == "w" || resultKey == "W")
            { W.Focus(); }
            else if (resultKey == "e" || resultKey == "E")
            { E1.Focus(); }
            else if (resultKey == "r" || resultKey == "R")
            { R.Focus(); }
            else if (resultKey == "t" || resultKey == "T")
            { T.Focus(); }
            else if (resultKey == "y" || resultKey == "Y")
            { Y.Focus(); }
            else if (resultKey == "u" || resultKey == "U")
            { U.Focus(); }
            else if (resultKey == "i" || resultKey == "I")
            { I.Focus(); }
            else if (resultKey == "o" || resultKey == "O")
            { O.Focus(); }
            else if (resultKey == "p" || resultKey == "P")
            { P.Focus(); }
            else if (resultKey == "a" || resultKey == "A")
            { A.Focus(); }
            else if (resultKey == "s" || resultKey == "S")
            { S.Focus(); }
            else if (resultKey == "d" || resultKey == "D")
            { D.Focus(); }
            else if (resultKey == "f" || resultKey == "F")
            { F.Focus(); }
            else if (resultKey == "g" || resultKey == "G")
            { G.Focus(); }
            else if (resultKey == "h" || resultKey == "H")
            { H.Focus(); }
            else if (resultKey == "j" || resultKey == "J")
            { J.Focus(); }
            else if (resultKey == "k" || resultKey == "K")
            { K.Focus(); }
            else if (resultKey == "l" || resultKey == "L")
            { L.Focus(); }
            else if (resultKey == "z" || resultKey == "Z")
            { Z.Focus(); }
            else if (resultKey == "x" || resultKey == "X")
            { X.Focus(); }
            else if (resultKey == "c" || resultKey == "C")
            { C.Focus(); }
            else if (resultKey == "v" || resultKey == "V")
            { V.Focus(); }
            else if (resultKey == "b" || resultKey == "B")
            { B.Focus(); }
            else if (resultKey == "n" || resultKey == "N")
            { N.Focus(); }
            else if (resultKey == "m" || resultKey == "M")
            { M.Focus(); }
            
            /////////////////////
            if (reciever == 1  && boolCapsLock)
            {
                /*InputText.Text = InputText.Text.Substring(0, InputText.Text.Length - 1);
                InputText.Select(InputText.Text.Length, 1);
                InputText.SelectedText = resultKey;
                //*/
                InputText.SelectedText = resultKey;
                InputText.Text += resultKey;
                
            }
            else if (reciever == 1 && !boolCapsLock )
            {
                /*InputText.Text = InputText.Text.Substring(0, InputText.Text.Length - 1);
                InputText.Select(InputText.Text.Length, 1);
                InputText.SelectedText = resultKey.ToLower(); 
                //*/InputText.Text += resultKey.ToLower();
            }
            else if (reciever == 2 && boolCapsLock)
            {
                //this.passText.Password += resultKey;
            }
            else if (reciever == 2 && !boolCapsLock)
            {
                //this.passText.Password += resultKey.ToLower();
            }
           
            this.Refresh();
        }

        private void Num2_Click(object sender, EventArgs e)
        {
            pressKey(Num2.Text);
        }

        private void Num3_Click(object sender, EventArgs e)
        {
            pressKey(Num3.Text);
        }

        private void Num4_Click(object sender, EventArgs e)
        {
            pressKey(Num4.Text);
        }

        private void Num5_Click(object sender, EventArgs e)
        {
            pressKey(Num5.Text);
        }

        private void Num6_Click(object sender, EventArgs e)
        {
            pressKey(Num6.Text);
        }

        private void Num7_Click(object sender, EventArgs e)
        {
            pressKey(Num7.Text);
        }

        private void Num8_Click(object sender, EventArgs e)
        {
            pressKey(Num8.Text);
        }

        private void Num9_Click(object sender, EventArgs e)
        {
            pressKey(Num9.Text);
        }

       
        private void Copy_Click(object sender, EventArgs e)
        {
            
            if (InputText.SelectedText.Length <= 1)
            {
                InputText.SelectAll();
            }
            InputText.Copy();
        }

        private void Cut_Click(object sender, EventArgs e)
        {
           
            if (InputText.SelectedText.Length <= 1)
            {
                InputText.SelectAll();
            }
            InputText.Cut();
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            
            InputText.Paste();
        }

        

        private void Q_Click(object sender, EventArgs e)
        {
            pressKey(Q.Text);
        }

        private void W_Click(object sender, EventArgs e)
        {
            pressKey(W.Text);
        }

        private void E1_Click(object sender, EventArgs e)
        {
            pressKey(E1.Text);
        }

        private void R_Click(object sender, EventArgs e)
        {
            pressKey(R.Text);
        }

        private void T_Click(object sender, EventArgs e)
        {
            pressKey(T.Text);
        }

        private void Y_Click(object sender, EventArgs e)
        {
            pressKey(Y.Text);
        }

        private void U_Click(object sender, EventArgs e)
        {
            pressKey(U.Text);
        }

        private void I_Click(object sender, EventArgs e)
        {
            pressKey(I.Text);
        }

        private void O_Click(object sender, EventArgs e)
        {
            pressKey(O.Text);
        }

        private void P_Click(object sender, EventArgs e)
        {
            pressKey(P.Text);
        }
        private void BackSpace_Click(object sender, EventArgs e)
        {
            if (reciever == 1 && InputText.Text.Length > 0)
            {
                InputText.Text = InputText.Text.Substring(0, InputText.Text.Length - 1);
                InputText.Select(InputText.Text.Length, 1);
            }
            //if (reciever == 2  && passText.Password.Length > 0)
            {
                //passText.Password = passText.Password.Substring(0, passText.Password.Length - 1);
                //passText.Password.Select(passText.Password.Length, 1);
            }
        }

        private void Num0_Click(object sender, EventArgs e)
        {
            pressKey(Num0.Text);
        }

        private void Tab_Click(object sender, EventArgs e)
        {
            pressKey("\t");
        }

        private void PrantsOpen_Click(object sender, EventArgs e)
        {
            pressKey("(");
        }

        private void PrantsClose_Click(object sender, EventArgs e)
        {
            pressKey(")");
        }

        private void N10_Click(object sender, EventArgs e)
        {
            pressKey("[");
        }

       
        private void Ri_Click(object sender, EventArgs e)
        {
            pressKey("]");
        }

        private void BraceOpen_Click(object sender, EventArgs e)
        {
            pressKey("{");
        }

        private void BraceClose_Click(object sender, EventArgs e)
        {
            pressKey("}");
        }

        private void AT_Click(object sender, EventArgs e)
        {
            pressKey("@");
        }

        private void A_Click(object sender, EventArgs e)
        {
            pressKey(A.Text);
        }

        private void S_Click(object sender, EventArgs e)
        {
            pressKey(S.Text);
        }

        private void D_Click(object sender, EventArgs e)
        {
            pressKey(D.Text);
        }

        private void F_Click(object sender, EventArgs e)
        {
            pressKey(F.Text);
        }

        private void G_Click(object sender, EventArgs e)
        {
            pressKey(G.Text);
        }

        private void H_Click(object sender, EventArgs e)
        {
            pressKey(H.Text);
        }

        private void J_Click(object sender, EventArgs e)
        {
            pressKey(J.Text);
        }

        private void K_Click(object sender, EventArgs e)
        {
            pressKey(K.Text);
        }

        private void L_Click(object sender, EventArgs e)
        {
            pressKey(L.Text);
        }

        private void Space_Click(object sender, EventArgs e)
        {
            pressKey(" ");
        }

       

        private void Semicolon_Click(object sender, EventArgs e)
        {
            pressKey(";");
        }

        private void BackSlash_Click(object sender, EventArgs e)
        {
            pressKey("\\");
        }

        private void SLASH_Click(object sender, EventArgs e)
        {
            pressKey("/");
        }

        private void Dollarsign_Click(object sender, EventArgs e)
        {
            pressKey("$");
        }

        private void VerticalBar_Click(object sender, EventArgs e)
        {
            pressKey("|");
        }

        private void Hatch_Click(object sender, EventArgs e)
        {
            pressKey("#");
        }

        private void Z_Click(object sender, EventArgs e)
        {
            pressKey(Z.Text);
        }

        private void X_Click(object sender, EventArgs e)
        {
            pressKey(X.Text);
        }

        private void C_Click(object sender, EventArgs e)
        {
            pressKey(C.Text);
        }

        private void V_Click(object sender, EventArgs e)
        {
            pressKey(V.Text);
        }

        private void B_Click(object sender, EventArgs e)
        {
            pressKey(B.Text);
        }

        private void N_Click(object sender, EventArgs e)
        {
            pressKey(N.Text);
        }

        private void M_Click(object sender, EventArgs e)
        {
            pressKey(M.Text);
        }

        private void QustionMark_Click(object sender, EventArgs e)
        {
            pressKey("?");
        }

        private void Camma_Click(object sender, EventArgs e)
        {
            pressKey(",");
        }

        private void Dot_Click(object sender, EventArgs e)
        {
            pressKey(".");
        }

        private void Prim_Click(object sender, EventArgs e)
        {
            pressKey("'");
        }

        private void DoublePrim_Click(object sender, EventArgs e)
        {
            pressKey("\"");
        }

        private void Exclamation_Click(object sender, EventArgs e)
        {
            pressKey("!");
        }

        private void Symbol_Click(object sender, EventArgs e)
        {
            pressKey("SUM");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            pressKey("<");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            pressKey(">");
        }

        private void Underscore_Click(object sender, EventArgs e)
        {
            pressKey("_");
        }

        private void tide_Click(object sender, EventArgs e)
        {
            pressKey("~");
        }

        private void Equals_Click(object sender, EventArgs e)
        {
            pressKey("=");
        }

        private void Caret_Click(object sender, EventArgs e)
        {
            pressKey("^");
        }

        private void And_Click(object sender, EventArgs e)
        {
            pressKey("&");
        }

        private void Plus_Click(object sender, EventArgs e)
        {
            pressKey("+");
        }

        private void Percent_Click(object sender, EventArgs e)
        {
            pressKey("%");
        }

        private void buttonDecimalSeparator_Click(object sender, EventArgs e)
        {
            pressKey(".");
        }

        private void Minus_Click(object sender, EventArgs e)
        {
            pressKey("-");
        }

        private void Times_Click(object sender, EventArgs e)
        {
            pressKey("*");
        }

        private void Divide_Click(object sender, EventArgs e)
        {
            pressKey("/");
        }

        private void Colon_Click(object sender, EventArgs e)
        {
            pressKey(":");
        }




        private void Keybord_FormClosed(object sender, FormClosedEventArgs e)
        {
            //InputText.Focus();
            //InputText.Refresh();
        }
        
        private void KeyKeyDown(object sender, KeyEventArgs e)
        {
                  
            
            
            switch (e.KeyCode)
            {
                case Keys.End:
                    if (InputText.Text.Length > 0)
                    {
                        InputText.SelectionStart = InputText.Text.Length;
                        InputText.SelectionLength = 0;
                    }
                    return;
                case Keys.Home:
                    InputText.SelectionStart = 0;
                    InputText.SelectionLength = 0;
                    return;
                                 
                case Keys.Space:
                    Space.PerformClick();
                    Space.Focus();
                    return;
                case Keys.Tab:
                    Tab.Focus();
                    Tab.PerformClick();
                    Tab.Focus();
                    return;
                case Keys.CapsLock:
                    CapsLock.PerformClick();
                    CapsLock.Focus();
                    return;
                case Keys.Back:
                        if (reciever == 1 && InputText.Text.Length > 0)
                        {
                            InputText.Text = InputText.Text.Substring(0, InputText.Text.Length - 1);
                            InputText.Select(InputText.Text.Length, 1);
                        }
                        //if (reciever == 2  && passText.Password.Length > 0)
                        {
                           // passText.Password = passText.Password.Substring(0, passText.Password.Length - 1);
                            //passText.Password.Select(passText.Password.Length, 1);
                        }
                    BackSpace.Focus();
                    return;
                case Keys.Add:
                    pressKey("+");
                    Plus.Focus();
                    return;
                case Keys.Decimal:
                    pressKey(".");
                    Decimals.Focus();
                    return;
                case Keys.Divide:
                    pressKey("/");
                    Divide.Focus();
                    return;
                case Keys.Multiply:
                    pressKey("*");
                    Times.Focus();
                    return;
                case Keys.OemBackslash:
                    pressKey("\\");
                    BackSlash.Focus();
                    return;
                case Keys.OemCloseBrackets:
                    if (e.Shift)
                    {
                        pressKey("}");
                        BraceClose.Focus();
                        return;
                    }
                    else
                    {
                        pressKey("]");
                        BracketClose.Focus();
                        return;
                    }
                case Keys.OemMinus:
                    pressKey("-");
                    Minus.Focus();
                    return;
                case Keys.OemOpenBrackets:
                    if (e.Shift)
                    {
                        pressKey("{");
                        BraceOpen.Focus();
                        return;
                    }
                    else
                    {
                        pressKey("[");
                        BracketOpen.Focus();
                        return;
                    }
                case Keys.OemPeriod:
                    if (e.Shift)
                    {
                        pressKey(">");
                        Greater.Focus();
                        return;
                    }
                    else
                    {
                        pressKey(".");
                        Dot.Focus();
                        return;
                    }
                case Keys.OemPipe:
                    if(e.Shift)
                    {
                        pressKey("|");
                        VerticalBar.Focus();
                        return;
                    }
                    else
                    {
                        pressKey("\\");
                        BackSlash.Focus();
                        return;
                    }
                case Keys.OemQuestion:
                    if (e.Shift)
                    {
                        pressKey("?");
                        QuestionMark.Focus();
                        return;
                    }
                    else
                    {
                        pressKey("/");
                        SLASH.Focus();
                        return;
                    }
                case Keys.OemQuotes:
                    if (e.Shift)
                    {
                        pressKey("\"");
                        DoublePrim.Focus();
                        return;
                    }
                    else
                    {
                        pressKey("\'");
                        Prim.Focus();
                        return;
                    }

                   
                case Keys.OemSemicolon:
                    if (e.Shift)
                    {
                        pressKey(":");
                        Colon.Focus();
                        return;
                    }
                    else
                    {
                        pressKey(";");
                        Semicolon.Focus();
                        return;
                    }
                case Keys.Oemcomma:
                    if (e.Shift)
                    {
                        pressKey("<");
                        Less.Focus();
                        return;
                    }
                    else
                    {
                        pressKey(",");
                        Camma.Focus();
                        return;
                    }
                case Keys.Oemplus:
                    pressKey("+");
                    Plus.Focus();
                    return;
                case Keys.Oemtilde:
                    if (e.Shift)
                    {
                        pressKey("~");
                        Tide.Focus();
                        return;
                    }
                    else
                    {
                        pressKey("'");
                        Prim.Focus();
                        return;
                    }
                case Keys.Separator:
                    pressKey("-"); 
                    return;
                case Keys.Subtract:
                    pressKey("-");
                    Minus.Focus();
                    return;
                case Keys.NumPad0:
                    pressKey("0");
                    Numb0.Focus();
                    return;
                case Keys.NumPad1:
                    pressKey("1");
                    Numb1.Focus();
                    return;
                case Keys.NumPad2:
                    pressKey("2");
                    Numb2.Focus();
                    return;
                case Keys.NumPad3:
                    pressKey("3");
                    Numb3.Focus();
                    return;
                case Keys.NumPad4:
                    pressKey("4");
                    Numb4.Focus();
                    return;
                case Keys.NumPad5:
                    pressKey("5");
                    Numb5.Focus();
                    return;
                case Keys.NumPad6:
                    pressKey("6");
                    Numb6.Focus();
                    return;
                case Keys.NumPad7:
                    pressKey("7");
                    Numb7.Focus();
                    return;
                case Keys.NumPad8:
                    pressKey("8");
                    Numb8.Focus();
                    return;
                case Keys.NumPad9:
                    pressKey("9");
                    Numb9.Focus();
                    return;
                case Keys.D0:
                    if (e.Shift == true)
                    {
                        pressKey(")");
                        PrantsClose.Focus();
                        return;
                    }
                    else
                    {
                        Num0.PerformClick();
                        Num0.Focus();
                        return;
                    }

                case Keys.D1:
                    if (e.Shift == true)
                    {
                        pressKey("!");
                        Exclamation.Focus();
                        return;
                    }
                    else
                    {
                        Num1.PerformClick();
                        Num1.Focus();
                        return;
                    }
                case Keys.D2:
                    if (e.Shift == true)
                    {
                        pressKey("@");
                        AT.Focus();
                        return;
                    }
                    else
                    {
                        Num2.PerformClick();
                        Num2.Focus();
                        return;
                    }
                case Keys.D3:
                    if (e.Shift == true)
                    {
                        pressKey("#");
                        Hatch.Focus();
                        return;
                    }
                    else
                    {
                        Num3.PerformClick();
                        Num3.Focus();
                        return;
                    }
                case Keys.D4:
                    if (e.Shift == true)
                    {
                        pressKey("$");
                        Dollarsign.Focus();
                        return;
                    }
                    else
                    {
                        Num4.PerformClick();
                        Num4.Focus();
                        return;
                    }
                case Keys.D5:
                    if (e.Shift == true)
                    {
                        pressKey("%");
                        Percent.Focus();
                        return;
                    }
                    else
                    {
                        Num5.PerformClick();
                        Num5.Focus();
                        return;
                    }
                case Keys.D6:
                    if (e.Shift == true)
                    {
                        pressKey("^");
                        Caret.Focus();
                        return;
                    }
                    else
                    {
                        Num6.PerformClick();
                        Num6.Focus();
                        return;
                    }
                case Keys.D7:
                    if (e.Shift == true)
                    {
                        pressKey("&");
                        And.Focus();
                        return;
                    }
                    else
                    {
                        Num7.PerformClick();
                        Num7.Focus();
                        return;
                    }
                case Keys.D8:
                    if (e.Shift == true)
                    {
                        pressKey("*");
                        Times.Focus();
                        return;
                    }
                    else
                    {
                        Num8.PerformClick();
                        Num8.Focus();
                        return;
                    }
                case Keys.D9:
                    if (e.Shift == true)
                    {
                        pressKey("(");
                        PrantsOpen.Focus();
                        return;
                    }
                    else
                    {
                        Num9.PerformClick();
                        Num9.Focus();
                        return;
                    }

                default:
                    if (e.KeyValue > 64 && e.KeyValue < 91)
                    {
                        pressKey("" + (char)e.KeyValue);
                    }
                    else if (e.KeyValue < 106 && e.KeyValue > 95)
                    {
                        pressKey("" + (char)e.KeyValue);
                    }
                    return;
            }
            
           // this.Focus();

        }

        private MainWindow main_win;   

        private login log_win;

        public void SetMainWindow(MainWindow main_wins)
        {
            log_win = null;
            main_win = main_wins;
        }


        public void SetLogWindow(login main_wins)
        {
            main_win = null;
            log_win = main_wins;
        }



        private void Camma_MouseHover(object sender, EventArgs e)
        {
            ShowTip(sender);
        }

        private void Dot_MouseHover(object sender, EventArgs e)
        {
            ShowTip(sender);
        }

        private void Exclamation_MouseHover(object sender, EventArgs e)
        {
            ShowTip(sender);
        }

        private void Hatch_MouseHover(object sender, EventArgs e)
        {
            ShowTip(sender);
        }

        private void VerticalBar_MouseMove(object sender, MouseEventArgs e)
        {
            ShowTip(sender);
        }

        private void VerticalBar_MouseHover(object sender, EventArgs e)
        {
            ShowTip(sender);
        }

        private void Semicolon_MouseHover(object sender, EventArgs e)
        {
            ShowTip(sender);
        }

        private void DoublePrim_MouseHover(object sender, EventArgs e)
        {
            ShowTip(sender);
        }

        private void Underscore_MouseHover(object sender, EventArgs e)
        {
            ShowTip(sender);
        }

        private void Enter1_MouseHover(object sender, EventArgs e)
        {
            //ShowTip(sender,"Press this button to run commands in the text box");
        }

        private void Keybord_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private int checkUrl = 1;

        public void setReciever(int val)
        {
            checkUrl = val;
        }

        private void checkEnterKey()
        {
            if (this.checkUrl == 1)
            {
                //log_win.enterKeyPressFromVirtualKey();
                main_win.UpdateAllTextBox();
                main_win.OpenBrowserWithURL();
                this.Close();
                //main_win.refeshBrowser();
            }
            else if (this.checkUrl == 2)
            {
                main_win.UpdateAllTextBox();
                main_win.refeshBrowser();
                this.Close();
                /*if (log_win.isLoginAvailable)
                {                    
                    log_win.loginByKeyboard();
                }*/

            }
        }

       

        private void Enter1_Click(object sender, EventArgs e)
        {
            //PARENT.DoCommand();

            checkEnterKey();
            
        }

        private void toolStripSplitButton1_DropDownClosed(object sender, EventArgs e)
        {
            this.Focus();
        }

      

        private void Keybord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar ==(char)13)
            {
                checkEnterKey();
               // MessageBox.Show("finally Enter");
            }
        }

        

        private void PreKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                
                Enter1.PerformClick();
                Enter1.Focus();
                return;
            }
            else if (e.KeyCode == Keys.Tab)
            {
                Tab.Focus();
                Tab.PerformClick();
                Tab.Focus();
                return;
            }
        }

        private void Keybord_FormClosing(object sender, FormClosingEventArgs e)
        {
           // e.Cancel = true;
            this.Hide();
        }


      

        public System.Windows.Controls.TextBox SetControl
        {
            get {
                
                return targetControl;
            }
            set {
                //if (value is System.Windows.Controls.TextBlock)
                {
                    reciever = 1;
                    InputText = (System.Windows.Controls.TextBox)value;
                }

                

                targetControl = value; 
                }
        }

        public System.Windows.Controls.PasswordBox SetPassControl
        {
            get {

                return passText;
            }
            set
            {
                //if (value is System.Windows.Controls.PasswordBox)
                {
                    reciever = 2;
                    passText = (System.Windows.Controls.PasswordBox)value;
                }

                //targetControl = value;
            }
        }
       
    }
}
