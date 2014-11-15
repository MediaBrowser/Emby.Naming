MediaBrowser.Naming
===================

A dependency-free, configurable file naming engine.

## Requirements:
- Parse video directories into objects containing metadata that can be determined from the file names
- For movies, extract the name, year, language, 3d format, multi-part info and quality level
- For episodes, extract all of the above plus episode numbers
- For subtitle files, extract the language, default and forced flags
- Support all Kodi movie naming feaures: http://kodi.wiki/vie...eo_files/Movies
- Support all Kodi tv naming features: http://kodi.wiki/vie..._files/TV_shows
- The consumer should be able to configure all features in the same way Kodi parsing can be configured, e.g. no fixed, hard-coded algorithms
- Extract extra files, e.g. trailers, theme songs, etc.
- Determine the relationship between files, e.g. movie has three parts, three subtitle files, three extra videos
- Support optional, injectable logging to write to the consumer's logging system
- Must be unit tested
- Must be a stand-alone library with zero dependencies