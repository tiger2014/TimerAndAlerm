using System.Drawing.Drawing2D;

namespace TimerAndAlerm
{
    public enum ButtonRole { Primary, Success, Danger, Neutral }

    public static class ThemeColors
    {
        // Classic Windows light theme — main form
        public static readonly Color FormBackground = SystemColors.Control;         // gray border
        public static readonly Color PanelBackground = SystemColors.Control;         // same gray as form
        public static readonly Color PanelBorder = Color.FromArgb(180, 180, 180);
        public static readonly Color TextPrimary = SystemColors.ControlText;     // Black
        public static readonly Color TextBoxBackground = SystemColors.Window;          // White
        public static readonly Color ProgressBarBack = SystemColors.Control;

        // Used by FullScreenMessageForm (full-screen alert overlay — keeps dramatic look)
        public static readonly Color ClockText = ColorTranslator.FromHtml("#00D4FF");
        public static readonly Color Primary = ColorTranslator.FromHtml("#6C63FF");
    }

    public static class ThemeHelper
    {
        public static void ApplyTheme(Form form)
        {
            form.BackColor = ThemeColors.FormBackground;
            form.ForeColor = ThemeColors.TextPrimary;
            form.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular);
            ApplyToControls(form.Controls);
        }

        private static void ApplyToControls(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                switch (ctrl)
                {
                    case Panel panel:
                        panel.BackColor = ThemeColors.PanelBackground;
                        panel.ForeColor = ThemeColors.TextPrimary;
                        break;

                    case Button btn:
                        StyleButton(btn, ButtonRole.Neutral);
                        break;

                    case Label lbl:
                        lbl.ForeColor = ThemeColors.TextPrimary;
                        lbl.BackColor = Color.Transparent;
                        break;

                    case TextBox txb:
                        txb.BackColor = ThemeColors.TextBoxBackground;
                        txb.ForeColor = ThemeColors.TextPrimary;
                        txb.BorderStyle = BorderStyle.Fixed3D;
                        break;

                    case NumericUpDown nud:
                        nud.BackColor = ThemeColors.TextBoxBackground;
                        nud.ForeColor = ThemeColors.TextPrimary;
                        nud.BorderStyle = BorderStyle.Fixed3D;
                        break;

                    case CheckBox chk:
                        chk.ForeColor = ThemeColors.TextPrimary;
                        chk.BackColor = Color.Transparent;
                        break;

                    case ProgressBar pb:
                        pb.BackColor = ThemeColors.ProgressBarBack;
                        break;
                }

                if (ctrl.HasChildren)
                {
                    ApplyToControls(ctrl.Controls);
                }
            }
        }

        public static void StyleButton(Button btn, ButtonRole role)
        {
            // White button with subtle gray border — matches the original UI
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.FromArgb(160, 160, 160);
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(229, 241, 251); // subtle blue hover
            btn.BackColor = Color.White;
            btn.ForeColor = SystemColors.ControlText;
            btn.Cursor = Cursors.Default;
            btn.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular);
        }

        public static void StyleSectionLabel(Label lbl)
        {
            lbl.ForeColor = ThemeColors.TextPrimary;
            lbl.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular);
        }
    }
}
