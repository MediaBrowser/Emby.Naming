using MediaBrowser.Naming.Common;
using System.Collections.Generic;

namespace MediaBrowser.Naming.Video
{
    /// <summary>
    /// Represents Media Browser video options - Kodi options with some additional features
    /// </summary>
    public class ExpandedVideoOptions : VideoOptions
    {
        public ExpandedVideoOptions()
        {
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
                }
            });

            ExtraRules.AddRange(new List<ExtraRule>
            {
                new ExtraRule
                {
                    ExtraType = "sample",
                    RuleType = ExtraRuleType.Suffix,
                    Token = "-sample",
                    MediaType = MediaType.Video
                },
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
        }
    }
}
