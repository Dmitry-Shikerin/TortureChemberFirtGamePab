using System;
using Sources.Presentation.UI.AudioSources;

namespace Sources.Infrastructure.Services
{
    public class BackgroundMusicService : IBackgroundMusicService
    {
        private readonly AudioSourceView _audioSourceView;

        public BackgroundMusicService(AudioSourceView audioSourceView)
        {
            _audioSourceView =
                audioSourceView ? audioSourceView : throw new ArgumentNullException(nameof(audioSourceView));
        }

        public void Enter(object payload = null)
        {
            _audioSourceView.Play();
        }

        public void Exit()
        {
            if (_audioSourceView == null)
                return;

            _audioSourceView.Stop();
        }
    }
}