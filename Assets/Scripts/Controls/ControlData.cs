using Assets.Scripts.Controls.Structures;

namespace Assets.Scripts.Controls {
    public class ControlData
    {
        PlayerControlType playerControls;
        public string HorizontalControl { get; private set; }
        public string VerticalControl { get; private set; }
        public string JumpControl { get; private set; }

        public ControlData(PlayerControlType playerControls)
        {
            this.playerControls = playerControls;
            SetControls();
        }


        void SetControls()
        {
            switch (playerControls)
            {
                case PlayerControlType.firstPlayer:
                    HorizontalControl = "Horizontal";
                    VerticalControl = "Vertical";
                    JumpControl = "Jump";
                    break;
                case PlayerControlType.secondPlayer:
                    HorizontalControl = "Horizontal2";
                    VerticalControl = "Vertical2";
                    JumpControl = "Jump2";
                    break;
                default:
                    HorizontalControl = "";
                    VerticalControl = "";
                    JumpControl = "";
                    break;
            }
        }



    }
}