# If you come from bash you might have to change your $PATH.
# export PATH=$HOME/bin:/usr/local/bin:$PATH

# Path to your oh-my-zsh installation.
export ZSH="$HOME/.oh-my-zsh"

# Set name of the theme to load --- if set to "random", it will
# load a random theme each time oh-my-zsh is loaded, in which case,
# to know which specific one was loaded, run: echo $RANDOM_THEME
# See https://github.com/ohmyzsh/ohmyzsh/wiki/Themes
ZSH_THEME="robbyrussell"

# Set list of themes to pick from when loading at random
# Setting this variable when ZSH_THEME=random will cause zsh to load
# a theme from this variable instead of looking in $ZSH/themes/
# If set to an empty array, this variable will have no effect.
# ZSH_THEME_RANDOM_CANDIDATES=( "robbyrussell" "agnoster" )

# Uncomment the following line to use case-sensitive completion.
# CASE_SENSITIVE="true"

# Uncomment the following line to use hyphen-insensitive completion.
# Case-sensitive completion must be off. _ and - will be interchangeable.
# HYPHEN_INSENSITIVE="true"

# Uncomment one of the following lines to change the auto-update behavior
# zstyle ':omz:update' mode disabled  # disable automatic updates
# zstyle ':omz:update' mode auto      # update automatically without asking
# zstyle ':omz:update' mode reminder  # just remind me to update when it's time

# Uncomment the following line to change how often to auto-update (in days).
# zstyle ':omz:update' frequency 13

# Uncomment the following line if pasting URLs and other text is messed up.
# DISABLE_MAGIC_FUNCTIONS="true"

# Uncomment the following line to disable colors in ls.
# DISABLE_LS_COLORS="true"

# Uncomment the following line to disable auto-setting terminal title.
# DISABLE_AUTO_TITLE="true"

# Uncomment the following line to enable command auto-correction.
# ENABLE_CORRECTION="true"

# Uncomment the following line to display red dots whilst waiting for completion.
# You can also set it to another string to have that shown instead of the default red dots.
# e.g. COMPLETION_WAITING_DOTS="%F{yellow}waiting...%f"
# Caution: this setting can cause issues with multiline prompts in zsh < 5.7.1 (see #5765)
# COMPLETION_WAITING_DOTS="true"

# Uncomment the following line if you want to disable marking untracked files
# under VCS as dirty. This makes repository status check for large repositories
# much, much faster.
# DISABLE_UNTRACKED_FILES_DIRTY="true"

# Uncomment the following line if you want to change the command execution time
# stamp shown in the history command output.
# You can set one of the optional three formats:
# "mm/dd/yyyy"|"dd.mm.yyyy"|"yyyy-mm-dd"
# or set a custom format using the strftime function format specifications,
# see 'man strftime' for details.
# HIST_STAMPS="mm/dd/yyyy"

# Would you like to use another custom folder than $ZSH/custom?
# ZSH_CUSTOM=/path/to/new-custom-folder

# Which plugins would you like to load?
# Standard plugins can be found in $ZSH/plugins/
# Custom plugins may be added to $ZSH_CUSTOM/plugins/
# Example format: plugins=(rails git textmate ruby lighthouse)
# Add wisely, as too many plugins slow down shell startup.
plugins=(git)

source $ZSH/oh-my-zsh.sh

# User configuration

# export MANPATH="/usr/local/man:$MANPATH"

# You may need to manually set your language environment
# export LANG=en_US.UTF-8

# Preferred editor for local and remote sessions
# if [[ -n $SSH_CONNECTION ]]; then
#   export EDITOR='vim'
# else
#   export EDITOR='mvim'
# fi

# Compilation flags
# export ARCHFLAGS="-arch x86_64"

# Set personal aliases, overriding those provided by oh-my-zsh libs,
# plugins, and themes. Aliases can be placed here, though oh-my-zsh
# users are encouraged to define aliases within the ZSH_CUSTOM folder.
# For a full list of active aliases, run `alias`.
#
# Example aliases
# alias zshconfig="mate ~/.zshrc"
# alias ohmyzsh="mate ~/.oh-my-zsh"

alias cat="bat"
alias node16='export PATH="/usr/local/bin:/usr/local/opt/node@16/bin:$PATH"; node -v'
alias node14='export PATH="/usr/local/opt/node@14/bin:$PATH"; node -v'export PATH="/opt/homebrew/opt/node@16/bin:$PATH"
export PATH="/opt/homebrew/opt/openjdk/bin:$PATH"


# Load Angular CLI autocompletion.
source <(ng completion script)

# The next line updates PATH for the Google Cloud SDK.
if [ -f '/Users/jeremycantu/Downloads/Development/GCP/google-cloud-sdk/path.zsh.inc' ]; then . '/Users/jeremycantu/Downloads/Development/GCP/google-cloud-sdk/path.zsh.inc'; fi

# The next line enables shell command completion for gcloud.
if [ -f '/Users/jeremycantu/Downloads/Development/GCP/google-cloud-sdk/completion.zsh.inc' ]; then . '/Users/jeremycantu/Downloads/Development/GCP/google-cloud-sdk/completion.zsh.inc'; fi

# NVM
export NVM_DIR=~/.nvm
source $(brew --prefix nvm)/nvm.sh

# custom gemini prompt invocation
gemini() {
    # Check if a prompt was provided
    if [ -z "$1" ]; then
        echo "Usage: gemini 'Your prompt here'"
        return 1
    fi

    # Listen for the exported environment variable
    if [ -z "$GEMINI_API_KEY" ]; then
        echo "Error: GEMINI_API_KEY environment variable is not set."
        echo "Make sure you have 'export GEMINI_API_KEY=\"your_key\"' in your ~/.zshrc."
        return 1
    fi
    local API_KEY="${GEMINI_API_KEY}"

    # Safely build the JSON payload
    local PAYLOAD=$(jq -n --arg text "$1" '{"contents":[{"parts":[{"text":$text}]}]}')
    
    # Create a temporary file to safely store the API response body
    local TMP_FILE=$(mktemp)
    
    echo "Thinking (Pro)..."

    # Use -o to send the JSON body to the temp file, and -w to output ONLY the HTTP status code
    local HTTP_STATUS=$(curl -s -o "$TMP_FILE" -w "%{http_code}" -H 'Content-Type: application/json' \
         -X POST \
         -d "$PAYLOAD" \
         "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-pro:generateContent?key=$API_KEY")

    # Check if the Pro limit was hit (HTTP 429)
    if [ "$HTTP_STATUS" -eq 429 ]; then
        # Overwrite the previous "Thinking" line
        echo -e "\033[1A\033[KThinking (Flash Fallback)..."
        
        # Fallback to the active Flash model
        HTTP_STATUS=$(curl -s -o "$TMP_FILE" -w "%{http_code}" -H 'Content-Type: application/json' \
             -X POST \
             -d "$PAYLOAD" \
             "https://generativelanguage.googleapis.com/v1beta/models/gemini-3.5-flash:generateContent?key=$API_KEY")
    fi

    # Clear the "Thinking..." line
    echo -e "\033[1A\033[K" 
    
    # Parse the final output directly from the temp file
    if [ "$HTTP_STATUS" -eq 200 ]; then
        # Success: Output the text
        jq -r '.candidates[0].content.parts[0].text // "Error: Could not parse response."' "$TMP_FILE"
    else
        # Failure: Output the specific API error
        echo "API Error (HTTP $HTTP_STATUS):"
        jq -r '.error.message // "Unknown error occurred."' "$TMP_FILE"
    fi
    
    # Clean up the temporary file so we don't clutter your /tmp directory
    rm -f "$TMP_FILE"
}
