using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AnimationUtilities
{
    public static class AnimationPlayer
    {
        public static void Play(Animation animation, AnimationClip clip)
        {
            ThrowIfAnimationIsNull(animation);

            if (clip == null)
            {
                return;
            }

            animation.Play(clip.name);
        }

        public static async UniTask PlayAsync(Animation animation, AnimationClip clip,
            CancellationToken cancellationToken = default)
        {
            ThrowIfAnimationIsNull(animation);

            if (clip == null)
            {
                return;
            }

            animation.Play(clip.name);

            await UniTask.WaitWhile(() => IsPlayingClip(animation, clip), PlayerLoopTiming.Update, cancellationToken);
        }

        private static void ThrowIfAnimationIsNull(Animation animation)
        {
            if (animation == null)
            {
                throw new ArgumentNullException();
            }
        }

        private static bool IsPlayingClip(Animation animation, AnimationClip clip)
        {
            return animation != null && animation.IsPlaying(clip.name);
        }
    }
}