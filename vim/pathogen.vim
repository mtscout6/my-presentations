" Run
" mkdir -p ~/.vim/autoload ~/.vim/bundle; \
" curl -LSso ~/.vim/autoload/pathogen.vim \
"    https://raw.github.com/tpope/vim-pathogen/master/autoload/pathogen.vim

" Add this to your ~/.vimrc
execute pathogen#infect()

" Now just clone vim plugins into ~/.vim/bundle
" cd ~/.vim/bundle
" git clone git://github.com/tpope/vim-sensible.git
