using System;
using System.Diagnostics;
using System.Threading;
using Core.Utils;
using Cysharp.Threading.Tasks;
using Messages;
using Models.Interfaces;
using UniRx;

namespace Models
{
    public sealed class TimeModel:  ITimeModel, IDisposable
    {
        CancellationTokenSource cts = new();
        private readonly CompositeDisposable _compositeDisposable = new();
        static CancellationToken token;
        private IMessageBroker _messageBroker;
        bool work = true;
        private readonly ReactiveProperty<int> _gameTime = new();
        public IObservable<int> GameTime =>  _gameTime;
        private IDisposable _messageDispose;

        public TimeModel(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        public void Initialize() { token = cts.Token; CountTime(token).Forget(); _messageDispose = _messageBroker.Receive<MoveFailedMessage>().Subscribe(OnMoveFailed).AddTo(_compositeDisposable); }

        public void OnMoveFailed(MoveFailedMessage message) => cts.Cancel();

        public void Dispose()
        {
            _messageDispose.Dispose();
        }

        private async UniTask CountTime(CancellationToken cancellationToken)
        {

            while (!cancellationToken.IsCancellationRequested)
            {
                await UniTask.Delay(NumericConstants.One * 1000);
                _gameTime.Value++;
            }
        }
    }
}