using Keyboard;

namespace Example
{

    public static class StateHelpers
    {
        /// <summary>
        /// The function to apply Upper/Lower text of buttons according to Shift/Tab state
        /// </summary>
        public static void ApplyState(VirtualKeyboard virtualKeyboard)
        {
            KeyboardLayout layout = new KeyboardLayout();

            var firstRow = layout.FirstRowButtonsDefault();
            ApplyLowerUpperText(virtualKeyboard, firstRow);
            virtualKeyboard.FirstRowCustomButtons = firstRow;

            var secondRow = layout.SecondRowButtonsDefault();
            ApplyLowerUpperText(virtualKeyboard, secondRow);
            virtualKeyboard.SecondRowCustomButtons = secondRow;

            var thirdRow = layout.ThirdRowButtonsDefault();
            ApplyLowerUpperText(virtualKeyboard, thirdRow);
            virtualKeyboard.ThirdRowCustomButtons = thirdRow;

            var fourthRow = layout.FourthRowButtonsDefault();
            ApplyLowerUpperText(virtualKeyboard, fourthRow);
            virtualKeyboard.FourthRowCustomButtons = fourthRow;
        }

        private static void ApplyLowerUpperText(VirtualKeyboard virtualKeyboard, ButtonsCollection buttons)
        {
            if (virtualKeyboard.ShiftState && virtualKeyboard.CapsLockState)
            {
                foreach (var item in buttons)
                {
                    item.TopText = item.TopText.ToLower();
                }
            }
            else if (virtualKeyboard.ShiftState || virtualKeyboard.CapsLockState)
            {
                foreach (var item in buttons)
                {
                    item.TopText = item.TopText.ToUpper();
                }
            }
            else
            {
                foreach (var item in buttons)
                {
                    item.TopText = item.TopText.ToLower();
                }
            }
        }
    }
}
