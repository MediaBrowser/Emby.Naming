using MediaBrowser.Naming.Video;
using System.Collections.Generic;

namespace MediaBrowser.Naming.Common
{
    public class NamingOptions
    {
        public List<string> AudioFileExtensions { get; set; }
        public List<string> AlbumStackingPrefixes { get; set; }
        
        public string[] SubtitleFileExtensions { get; set; }
        public char[] SubtitleFlagDelimiters { get; set; }

        public string[] SubtitleForcedFlags { get; set; }
        public string[] SubtitleDefaultFlags { get; set; }

        public List<EpisodeExpression> EpisodeExpressions { get; set; }
        public List<string> EpisodeWithoutSeasonExpressions { get; set; }
        public List<string> EpisodeMultiPartExpressions { get; set; }

        public List<string> VideoFileExtensions { get; set; }
        public List<string> StubFileExtensions { get; set; }

        public List<StubTypeRule> StubTypes { get; set; }

        public char[] VideoFlagDelimiters { get; set; }
        public List<Format3DRule> Format3DRules { get; set; }

        public List<string> VideoFileStackingExpressions { get; set; }
        public List<string> CleanDateTimes { get; set; }
        public List<string> CleanStrings { get; set; }

        public List<ExtraRule> VideoExtraRules { get; set; }
        
        public NamingOptions()
        {
            VideoFileExtensions = new List<string>()
            {
                ".m4v",
                ".3gp", 
                ".nsv", 
                ".ts", 
                ".ty", 
                ".strm", 
                ".rm", 
                ".rmvb", 
                ".m3u", 
                ".ifo", 
                ".mov", 
                ".qt", 
                ".divx", 
                ".xvid", 
                ".bivx", 
                ".vob", 
                ".nrg", 
                ".img",
                ".iso", 
                ".pva", 
                ".wmv", 
                ".asf", 
                ".asx", 
                ".ogm", 
                ".m2v", 
                ".avi", 
                ".bin", 
                ".dat", 
                ".dvr-ms", 
                ".mpg", 
                ".mpeg", 
                ".mp4", 
                ".mkv", 
                ".avc", 
                ".vp3", 
                ".svq3", 
                ".nuv", 
                ".viv", 
                ".dv", 
                ".fli", 
                ".flv", 
                ".rar", 
                ".001", 
                ".wpl", 
                ".zip"
            };

            VideoFlagDelimiters = new[]
            {
                '(', 
                ')', 
                '-', 
                '.', 
                '_', 
                '[', 
                ']'
            };

            StubFileExtensions = new List<string>
            {
                ".disc"
            };

            StubTypes = new List<StubTypeRule>
            {
                new StubTypeRule
                {
                     StubType = "dvd",
                     Token = "dvd"
                },
                new StubTypeRule
                {
                     StubType = "hddvd",
                     Token = "hddvd"
                },
                new StubTypeRule
                {
                     StubType = "bluray",
                     Token = "bluray"
                },
                new StubTypeRule
                {
                     StubType = "bluray",
                     Token = "brrip"
                },
                new StubTypeRule
                {
                     StubType = "bluray",
                     Token = "bd25"
                },
                new StubTypeRule
                {
                     StubType = "bluray",
                     Token = "bd50"
                },
                new StubTypeRule
                {
                     StubType = "vhs",
                     Token = "vhs"
                },
                new StubTypeRule
                {
                     StubType = "tv",
                     Token = "HDTV"
                },
                new StubTypeRule
                {
                     StubType = "tv",
                     Token = "PDTV"
                },
                new StubTypeRule
                {
                     StubType = "tv",
                     Token = "DSR"
                }
            };

            VideoFileStackingExpressions = new List<string>
            {
                "(.*?)([ _.-]*(?:cd|dvd|p(?:(?:ar)?t)|dis[ck]|d)[ _.-]*[0-9]+)(.*?)(\\.[^.]+)$",
                "(.*?)([ _.-]*(?:cd|dvd|p(?:(?:ar)?t)|dis[ck]|d)[ _.-]*[a-d])(.*?)(\\.[^.]+)$",
                "(.*?)([ ._-]*[a-d])(.*?)(\\.[^.]+)$"
            };

            CleanDateTimes = new List<string>
            {
                @"(.+[^ _\,\.\(\)\[\]\-])[ _\.\(\)\[\]\-]+(19[0-9][0-9]|20[0-1][0-9])([ _\,\.\(\)\[\]\-][^0-9]|$)"
            };

            CleanStrings = new List<string>
            {
                @"[ _\,\.\(\)\[\]\-](ac3|dts|custom|dc|divx|divx5|dsr|dsrip|dutch|dvd|dvdrip|dvdscr|dvdscreener|screener|dvdivx|cam|fragment|fs|hdtv|hdrip|hdtvrip|internal|limited|multisubs|ntsc|ogg|ogm|pal|pdtv|proper|repack|rerip|retail|cd[1-9]|r3|r5|bd5|se|svcd|swedish|german|read.nfo|nfofix|unrated|ws|telesync|ts|telecine|tc|brrip|bdrip|480p|480i|576p|576i|720p|720i|1080p|1080i|hrhd|hrhdtv|hddvd|bluray|x264|h264|xvid|xvidvd|xxx|www.www|\[.*\])([ _\,\.\(\)\[\]\-]|$)",
                @"(\[.*\])"
            };

            SubtitleFileExtensions = new[]
            {
                ".srt", 
                ".ssa", 
                ".ass", 
                ".sub"
            };

            SubtitleFlagDelimiters = new[]
            {
                '.'
            };

            SubtitleForcedFlags = new[]
            {
                "foreign",
                "forced"
            };

            SubtitleDefaultFlags = new[]
            {
                "default"
            };

            AlbumStackingPrefixes = new List<string>
            {
                "disc", 
                "cd", 
                "disk", 
                "vol", 
                "volume"
            };

            AudioFileExtensions = new List<string>()
            {
                ".nsv",
                ".m4a", 
                ".flac", 
                ".aac", 
                ".strm", 
                ".pls", 
                ".rm", 
                ".mpa", 
                ".wav", 
                ".wma", 
                ".ogg", 
                ".mp3", 
                ".mp2", 
                ".m3u", 
                ".mod", 
                ".amf", 
                ".669", 
                ".dmf", 
                ".dsm", 
                ".far", 
                ".gdm", 
                ".imf", 
                ".it", 
                ".m15", 
                ".med", 
                ".okt", 
                ".s3m", 
                ".stm", 
                ".sfx", 
                ".ult", 
                ".uni", 
                ".xm", 
                ".sid", 
                ".ac3", 
                ".dts", 
                ".cue", 
                ".aif", 
                ".aiff", 
                ".wpl", 
                ".ape", 
                ".mac", 
                ".mpc", 
                ".mp+", 
                ".mpp", 
                ".shn", 
                ".zip", 
                ".rar", 
                ".wv", 
                ".nsf", 
                ".spc", 
                ".gym", 
                ".adplug", 
                ".adx", 
                ".dsp", 
                ".adp", 
                ".ymf", 
                ".ast", 
                ".afc", 
                ".hps", 
                ".xsp",
                ".acc",
                ".m4b",
                ".oga"
            };

            EpisodeExpressions = new List<EpisodeExpression>
            {
                new EpisodeExpression("s([0-9]+)[ ._-]*e([0-9]+(?:(?:[a-i]|\\.[1-9])(?![0-9]))?)([^\\\\/]*)$"), 
                new EpisodeExpression("[\\._ -]()e(?:p[ ._-]?)?([0-9]+(?:(?:[a-i]|\\.[1-9])(?![0-9]))?)([^\\\\/]*)$"), 
                new EpisodeExpression("([0-9]{4})[\\.-]([0-9]{2})[\\.-]([0-9]{2})", true)
                {
                    DateTimeFormats = new []
                    {
                        "yyyy.MM.dd",
                        "yyyy-MM-dd",
                        "yyyy_MM_dd"
                    }
                }, 
                new EpisodeExpression("([0-9]{2})[\\.-]([0-9]{2})[\\.-]([0-9]{4})", true)
                {
                    DateTimeFormats = new []
                    {
                        "dd.MM.yyyy",
                        "dd-MM-yyyy",
                        "dd_MM_yyyy"
                    }
                }, 
                new EpisodeExpression("[\\\\/\\._ \\[\\(-]([0-9]+)x([0-9]+(?:(?:[a-i]|\\.[1-9])(?![0-9]))?)([^\\\\/]*)$"), 
                new EpisodeExpression("[\\\\/\\._ -]([0-9]+)([0-9][0-9](?:(?:[a-i]|\\.[1-9])(?![0-9]))?)([\\._ -][^\\\\/]*)$"), 
                new EpisodeExpression("[\\/._ -]p(?:ar)?t[_. -]()([ivx]+|[0-9]+)([._ -][^\\/]*)$")
            };

            EpisodeWithoutSeasonExpressions = new List<string>
            {
                @"[/\._ \-]()([0-9]+)(-[0-9]+)?"
            };

            EpisodeMultiPartExpressions = new List<string>
            {
                @"^[-_ex]+([0-9]+(?:(?:[a-i]|\\.[1-9])(?![0-9]))?)"
            };

            VideoExtraRules = new List<ExtraRule>
            {
                new ExtraRule
                {
                    ExtraType = "trailer",
                    RuleType = ExtraRuleType.Filename,
                    Token = "trailer",
                    MediaType = MediaType.Video
                },
                new ExtraRule
                {
                    ExtraType = "trailer",
                    RuleType = ExtraRuleType.Suffix,
                    Token = "-trailer",
                    MediaType = MediaType.Video
                },
                new ExtraRule
                {
                    ExtraType = "themesong",
                    RuleType = ExtraRuleType.Filename,
                    Token = "theme",
                    MediaType = MediaType.Audio
                }
            };
            Format3DRules = new List<Format3DRule>
            {
                // Kodi rules:
                new Format3DRule
                {
                    PreceedingToken = "3d",
                    Token = "hsbs"
                },
                new Format3DRule
                {
                    PreceedingToken = "3d",
                    Token = "sbs"
                },
                new Format3DRule
                {
                    PreceedingToken = "3d",
                    Token = "htab"
                },
                new Format3DRule
                {
                    PreceedingToken = "3d",
                    Token = "tab"
                }
            };

        }
    }
}
