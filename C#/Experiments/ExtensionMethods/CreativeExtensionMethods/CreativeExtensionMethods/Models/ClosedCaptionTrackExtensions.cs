using System;
using System.Collections.Generic;
using System.Linq;

namespace CreativeExtensionMethods.Models
{
    public class ClosedCaption
    {
        /// <summary>
        /// Display text
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// When text gets display relative to beginning of the track
        /// </summary>
        public TimeSpan Offset { get; }

        /// <summary>
        /// How long it gets displayed
        /// </summary>
        public TimeSpan Duration { get; set; }

        public ClosedCaption(string text, TimeSpan offset, TimeSpan duration)
        {
            Text = text;
            Offset = offset;
            Duration = duration;
        }
    }

    public class ClosedCaptionTrack
    {
        /// <summary>
        /// Language of the closed captions
        /// </summary>
        public string Language { get; }

        /// <summary>
        /// Collection of closed captions
        /// </summary>
        public IReadOnlyList<ClosedCaption> ClosedCaptions { get; }

        public ClosedCaptionTrack(string language, IReadOnlyList<ClosedCaption> captions)
        {
            Language = language;
            ClosedCaptions = captions;
        }
    }

    /// <summary>
    /// Refactoring model classes
    /// </summary>
    public static class ClosedCaptionTrackExtensions
    {
        /// <summary>
        /// Benefits of this pattern:
        /// 1. Clear it only works with public members, doesn't mutate private state
        /// 2. Provides a shortcut and is here for developer's benefit
        /// 3. Part of an entirely separate class
        /// </summary>
        /// <param name="track"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static ClosedCaption GetByTime(this ClosedCaptionTrack track, TimeSpan time) =>
            track.ClosedCaptions.FirstOrDefault(cc => cc.Offset <= time && cc.Offset + cc.Duration >= time);
    }
}