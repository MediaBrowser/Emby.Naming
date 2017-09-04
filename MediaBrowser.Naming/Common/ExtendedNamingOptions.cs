using MediaBrowser.Naming.Video;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaBrowser.Naming.Common
{
    /// <summary>
    /// Represents Media Browser options - Kodi options with some additional features
    /// </summary>
    public class ExtendedNamingOptions : NamingOptions
    {
        public ExtendedNamingOptions()
        {
            var extensions = VideoFileExtensions.ToList();

            extensions.AddRange(new[]
            {
                ".mkv",
                ".m2t",
                ".m2ts",
                ".img",
                ".iso",
                ".mk3d",
                ".ts",
                ".rmvb",
                ".mov",
                ".avi",
                ".mpg",
                ".mpeg",
                ".wmv",
                ".mp4",
                ".divx",
                ".dvr-ms",
                ".wtv",
                ".ogm",
                ".ogv",
                ".asf",
                ".m4v",
                ".flv",
                ".f4v",
                ".3gp",
                ".webm",
                ".mts",
                ".m2v",
                ".rec",
                ".mxf"
            });

            // Problematic. Can always become configurable if needed.
            extensions.Remove(".dat");
            extensions.Remove(".wpl");
            extensions.Remove(".m3u");

            VideoFileExtensions = extensions
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            VideoExtraRules.AddRange(new List<ExtraRule>
            {
                new ExtraRule
                {
                    ExtraType = "scene",
                    RuleType = ExtraRuleType.Suffix,
                    Token = "-scene",
                    MediaType = MediaType.Video
                },
                new ExtraRule
                {
                    ExtraType = "clip",
                    RuleType = ExtraRuleType.Suffix,
                    Token = "-clip",
                    MediaType = MediaType.Video
                },
                new ExtraRule
                {
                    ExtraType = "interview",
                    RuleType = ExtraRuleType.Suffix,
                    Token = "-interview",
                    MediaType = MediaType.Video
                },
                new ExtraRule
                {
                    ExtraType = "behindthescenes",
                    RuleType = ExtraRuleType.Suffix,
                    Token = "-behindthescenes",
                    MediaType = MediaType.Video
                },
                new ExtraRule
                {
                    ExtraType = "deletedscene",
                    RuleType = ExtraRuleType.Suffix,
                    Token = "-deleted",
                    MediaType = MediaType.Video
                }
            });
            
            Format3DRules.AddRange(new List<Format3DRule>
            {
                 // Media Browser rules:
                new Format3DRule
                {
                    Token = "fsbs"
                },
                new Format3DRule
                {
                    Token = "hsbs"
                },
                new Format3DRule
                {
                    Token = "sbs"
                },
                new Format3DRule
                {
                    Token = "ftab"
                },
                new Format3DRule
                {
                    Token = "htab"
                },
                new Format3DRule
                {
                    Token = "tab"
                },
                new Format3DRule
                {
                    Token = "sbs3d"
                },
                new Format3DRule
                {
                    Token = "mvc"
                }
            });
        }
    }
}
