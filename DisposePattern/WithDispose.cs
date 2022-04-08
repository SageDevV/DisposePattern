using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisposePattern
{
    class WithDispose : IDisposable
    {
        private Stopwatch stopwatch = null;
        private bool disposed = false;
        public WithDispose()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }
        public void DoWork()
        {
            double j = 0;
            for (int i = 0; i < 1000; i++)
            {
                j += i * i;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                stopwatch.Stop();
                Interlocked.Increment(ref Program.FinalisedObjects);
                Interlocked.Add(ref Program.TotalTime, stopwatch.ElapsedMilliseconds);

                if (disposing)
                {
                    // Chamada explícita do Usuário.
                    // Basicamente não precisa fazer mais nada.
                }
                else
                {
                    // Chamada pelo Finalizer.
                    // Não pode referenciar, é mais performático.
                }
                disposed = true;
            }
        }

        ~WithDispose()
        {
            Dispose(false);
        }
    }
}
