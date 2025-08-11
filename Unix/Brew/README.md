## Brewfiles

Brewfiles are files generated with definitions that Homebrew reads and processes, a generated 
Brewfile looks similar to the below:
 
 ```
 tap "heroku/brew"
 tap "homebrew/bundle"
 tap "homebrew/cask"
 tap "homebrew/cask-versions"
 tap "homebrew/core"
 tap "homebrew/services"
 brew "borgmatic"
 cask "iterm2-beta"
 ```

You can then run the following command to restore all taps, programs and cask-based applications in one go:

`brew bundle`

## Backup
 
Simply run the following command to set up the repository

`brew tap Homebrew/bundle`

Once complete, you can then perform a single command to backup to your home directory.

`brew bundle dump`

By default, the Brewfile will be located at `~/Brewfile`

## Restoring
 
As mentioned before you can run the brew bundle command to restore from the backup but this will only restore from `~/Brewfile`

You can use specific arguments to choose your Brewfile location

```
brew bundle --help
--file                        Read the Brewfile from this location. Use
                                      --file=- to pipe to stdin/stdout.
 Using --file  will allow you to select different Brewfiles. An example of this can be selecting a 
 file from a different snapshot.
```

- Example: `brew bundle --file=~/Brewfile-20200531`