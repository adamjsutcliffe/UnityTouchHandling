using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TweenScript : MonoBehaviour
{
    /// <summary>
    /// The time scale.
    /// </summary>
    public float timeScale = 1.0f;

    public AnimationCurve TileBounceCurve;

    /// <summary>
    /// The animations.
    /// </summary>
    protected List<global::TweenAnimation.Models.TweenAnimation> animations =
        new List<global::TweenAnimation.Models.TweenAnimation>();

    /// <summary>
    /// The animations to enqeue.
    /// </summary>
    protected List<global::TweenAnimation.Models.TweenAnimation> animationsToEnqeue =
        new List<global::TweenAnimation.Models.TweenAnimation>();

    /// <summary>
    /// The instance.
    /// </summary>
    public static TweenScript Instance { get; protected set; }

    /// <summary>
    /// Start this instance.
    /// </summary>
    void Awake()
    {
        TweenScript.Instance = this;
    }

    /// <summary>
    /// Fixeds the update.
    /// </summary>
    void Update()
    {
        var finishedCount = 0;

        animations.AddRange(animationsToEnqeue);
        animationsToEnqeue.Clear();

        foreach (var animation in animations)
        {
            animation.Update();

            if (animation.IsFinished)
            {
                finishedCount++;
                animation.Finished();
            }
        }

        if (finishedCount > 0)
        {
            this.animations = this.animations.Where(a => !a.IsFinished).ToList();
        }
    }

    /// <summary>
    /// Enqueue the specified animation.
    /// </summary>
    /// <param name="animation">Animation.</param>
    public void Enqueue(global::TweenAnimation.Models.TweenAnimation animation)
    {
        animationsToEnqeue.Add(animation);
        animation.Started();
    }

    /// <summary>
    /// Dequeue the specified animation.
    /// </summary>
    /// <param name="animation">Animation.</param>
    public void Dequeue(global::TweenAnimation.Models.TweenAnimation animation)
    {
        animations.Remove(animation);
        animationsToEnqeue.Remove(animation);
    }

    /// <summary>
    /// Dequeue the specified animations.
    /// </summary>
    /// <param name="animationsToRemove">Animations.</param>
    public void Dequeue(List<global::TweenAnimation.Models.TweenAnimation> animationsToRemove)
    {
        animationsToRemove.ForEach(x => animations.Remove(x));
        animationsToRemove.ForEach(x => animationsToEnqeue.Remove(x));
    }
}