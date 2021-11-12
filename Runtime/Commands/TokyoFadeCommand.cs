using Tokyo.Fader;

namespace Tokyo.Command {

    public class TokyoFadeCommand : BaseCommand {

        private readonly FadeConfig _fadeConfig;

        public TokyoFadeCommand(FadeConfig fadeConfig) {
            _fadeConfig = fadeConfig;
        }

        protected override void ExecInternal() {
            TokyoFader.Instance().OnFadeComplete += OnFadeComplete;
            TokyoFader.Instance().Fade(_fadeConfig);
        }

        private void OnFadeComplete() {
            TokyoFader.Instance().OnFadeComplete -= OnFadeComplete;
            NotifyComplete();
        }
    }
}