# Vim plugins

Vim is by default a very bare text editor that presents means to alter text efficiently.

However, it is extensible via it's own language VimScript

There are many plugins that you can take advantage of, most of which can be found at [vim.org](http://www.vim.org/scripts)

## Plugin Management

Two popular managers:
- Vundle
- Pathogen

## Airline

More aesthetically pleasing status bar

## Ctags

Parses code files and generates a meaningful "tag" file which is used by vim to "jump to definition"

Don't use the default shipped with mac. Instead install with `brew install ctags` and ensure `/usr/local/bin` is first in your path

Since you have to run the `ctags` command to update the "tags" file I use git commit hooks and the vim-autotag plugin to help me out

Shameless plug: [Craig Emery](https://github.com/craigemery) Originally wrote a vim plugin to do the auto tagging on save, but I ran into problems with it being embedded within his dotfiles repo. So I use my [own](https://github.com/mtscout6/vim-ctags-autotag)

## Ctrl-P

Quick file lookup, and more...

## Ack

Cli tool like grep but built for programmers
Respects file types

## Quickfix reflector

Save changes to many files at once

## Multi-cursor

Rename a variable in many places at once

## Git

### Vim-Fugitive

Perform git operations in git: Blame, log, write/stage, commit

### Git-Gutter

Show line changes in line gutter

Revert, commit, preview hunk changes

## Surround

Surround things with {}, (), [], "", '', <xml></xml>

## Eclim & YouCompleteMe

Intellisense for vim

## Vim Script

[Excellent resource](http://learnvimscriptthehardway.stevelosh.com/)

Variables ??

Prefix     Meaning
---------------------------------------------------------------------
g:varname  The variable is global
s:varname  The variable is local to the current script file
w:varname  The variable is local to the current editor window
t:varname  The variable is local to the current editor tab
b:varname  The variable is local to the current editor buffer
l:varname  The variable is local to the current function
a:varname  The variable is a parameter of the current function
v:varname  The variable is one that Vim predefines

Psuedovariables

Prefix       Meaning
---------------------------------------------------------------------
&varname     A Vim option (local option if defined, otherwise global)
&l:varname   A local Vim option
&g:varname   A global Vim option
@varname     A Vim register
$varname     An environment variable

## Check out my [dotfiles](https://github.com/mtscout6/dotfiles)
