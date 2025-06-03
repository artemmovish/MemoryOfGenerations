using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Services
{
    public class AudioService : IDisposable
    {
        private WaveOutEvent _waveOut;
        private AudioFileReader _audioFileReader;
        private string _currentFilePath;

        public bool IsPlaying => _waveOut?.PlaybackState == PlaybackState.Playing;
        public bool IsPaused => _waveOut?.PlaybackState == PlaybackState.Paused;
        public TimeSpan CurrentTime => _audioFileReader?.CurrentTime ?? TimeSpan.Zero;
        public TimeSpan TotalTime => _audioFileReader?.TotalTime ?? TimeSpan.Zero;

        // Загружает аудиофайл по указанному пути
        public void LoadAudio(string filePath)
        {
            DisposeCurrentAudio();

            _audioFileReader = new AudioFileReader(filePath);
            _waveOut = new WaveOutEvent();
            _waveOut.Init(_audioFileReader);
            _currentFilePath = filePath;
        }

        // Воспроизводит загруженный файл
        public void Play()
        {
            if (_waveOut == null)
                throw new InvalidOperationException("Аудиофайл не загружен!");

            _waveOut.Play();
        }

        // Ставит на паузу
        public void Pause()
        {
            if (_waveOut == null)
                throw new InvalidOperationException("Аудиофайл не загружен!");

            _waveOut.Pause();
        }

        // Останавливает воспроизведение и сбрасывает позицию
        public void Stop()
        {
            if (_waveOut == null)
                throw new InvalidOperationException("Аудиофайл не загружен!");

            _waveOut.Stop();
            _audioFileReader.Position = 0;
        }

        // Перематывает на указанное время (в секундах)
        public void Seek(double seconds)
        {
            if (_audioFileReader == null)
                throw new InvalidOperationException("Аудиофайл не загружен!");

            var time = TimeSpan.FromSeconds(seconds);
            if (time > _audioFileReader.TotalTime)
                time = _audioFileReader.TotalTime;

            _audioFileReader.CurrentTime = time;
        }

        // Освобождает ресурсы
        public void Dispose()
        {
            DisposeCurrentAudio();
        }

        private void DisposeCurrentAudio()
        {
            _waveOut?.Stop();
            _waveOut?.Dispose();
            _audioFileReader?.Dispose();
            _waveOut = null;
            _audioFileReader = null;
            _currentFilePath = null;
        }
    }
}
