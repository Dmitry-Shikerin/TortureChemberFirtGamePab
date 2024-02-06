using Sources.Controllers.UI.AudioSources.Common;
using Sources.DomainInterfaces.UI;
using Sources.PresentationInterfaces.UI.AudioSources;

namespace Sources.Infrastructure.Factories.Controllers.UI.AudioSourceUIPresenters
{
    public class AudioSourceUIPresenterFactory
    {
        public AudioSourceUIPresenter Create(IAudioSourceActivator audioSourceActivator,
            IAudioSourceUI audioSourceUI)
        {
            return new AudioSourceUIPresenter(audioSourceActivator, audioSourceUI);
        }
    }
}