using System;
using System.Threading;
using Core.Utils;
using Cysharp.Threading.Tasks;
using Models.Interfaces;
using UniRx;

namespace Models
{
    public sealed class TimeModel:  ITimeModel, IDisposable
    {
        CancellationTokenSource cts = new();

        private readonly ReactiveProperty<int> _gameTime = new();
        public IObservable<int> GameTime =>  _gameTime;

        public void Initialize() => CountTime(cts).Forget();

        public void Dispose() => cts.Cancel();

        private async UniTask CountTime(CancellationTokenSource cancel)
        {
            bool Work = true;
            try
            {
                Work = true;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operation was canceled");
                Work = false;
            }

            while (Work)
            {
                await UniTask.Delay(NumericConstants.One * 1000);
                _gameTime.Value++;
            }
        }
    }
}