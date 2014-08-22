# NPM

### Node Package Manager

Matt Smith - [@mtscout6](http://twitter.com/mtscout6)

---

### What is an npm package?

<hr />

1. a folder containing a program described by a package.json file
2. a gzipped tarball containing (1)
3. a url that resolves to (2)
4. a `<name>@<version>` that is published on the registry with (3)
5. a `<name>@<tag>` that points to (4)
6. a `<name>` that has a "latest" tag satisfying (5)
7. a `git` url that, when cloned, results in (1).

----

### NPM Packages do not have to be NodeJS Modules!

* Less
* Sass
* Client Side Scripts
* Images
* Fonts
* Even C# source or compiled
* Use your imagination

----

### However, it was designed primarily for NodeJS Modules!

---

### You define an NPM package with a package.json file at your project's root

----

#### Minimum requirements

```json
{
  "name": "package-name",
  "version": "0.1.0"
}
```

----

#### From your project's root run

```bash
$ npm init
```

<br />

_`init` will only cover the most common items, and tries to guess sane defaults._

----

### The sane defaults work best if you first run

```bash
$ git init
$ git remote add origin <remote-host>
```

----

#### Useful shell commands based on package.json

```bash
$ npm docs
$ npm bugs
```

---

## package.json Scripts

----

## Special Scripts

<br />

* install
* update
* uninstall
* publish
* restart
* start
* stop
* test

<br />

_Each comes with a corresponding pre and post script hook <br /> (ie pretest and posttest)_

----

# Install scripts are an anti-pattern

----

#### Install scripts were initially used for compilation on the target architecture

<br />

<h4 class='fragment'>
  The new world order is via `node-gyp` scripts which are files in your project's root with the extension `.gyp`
</h4>

----

### Prepublish

#### Highly recommended when your _"install"_ is not dependent on the operating system or architecture of the target system

<ul class='fragment'>
  <li>Compile CoffeeScript to JavaScript</li>
  <li>Create minified versions of JavaScript</li>
  <li>Fetching remote resources used by your package</li>
</ul>

----

### postinstall

```json
  ...
  "scripts": {
    "postinstall": "napa"
  },
  "napa": {
    "jQueryMask": "igorescobar/jQuery-Mask-Plugin"
  },
  "devDependencies": {
    "napa": "^0.4.1",
  },
  ...
```

----

### install

Default based on package contents

```
    ...
    "start": "node server.js"
    ...
```

If `server.js` exists in the package root.

_Useful for pushing to Heroku_

----

### Arbitrary scripts can also be run

```bash
$ npm run-script <script-name>
```

----

### Example arbitrary script

```
  ...
  "scripts": {
    "coveralls": "istanbul cover _mocha --report lcovonly -- -R spec && cat ./coverage/lcov.info | coveralls && rm -rf ./coverage"
  },
  ...
```

----

### Why put scripts in package.json?

<br />

### We already use WebPack, Gulp, Grunt, or Mimosa!

----

### #1 Reason

#### PATH environment

```
{
  "name" : "foo",
  "dependencies" : {
    "bar" : "0.1.x"
  },
  "scripts": {
    "start" : "bar ./test"
  }
}
```

<p class='fragment'>The <code>bar</code> script gets installed to <code>./node_modules/.bin/</code> which is not normally in your path.</p>
<p class='fragment'>Your CI Server does not need to globally install modules.</p>

----

### Scripts can be any type of executable

---

### Dependencies

Modules you depend on for your application to run

<br />

This is no different then other languages with support like Nuget, or RubyGems

----

### Dependency Hell isn't as hot

```bash
├── log4net@1.2.10.0
└─┬ some-other-module@1.2.3
  └── log4net@1.2.12.0
```

Take the problem from .Net when log4net changed their public key token as an example

----

### Module loading

When the module identifier passed to `require()` is not:

<br />

* a native module
* begin with `'/'`, `'../'`, or `'./'`

<br />

Node starts at the parent directory of the current module, and adds `/node_modules`, and attempts to load the module from that location.

----

If it is not found there, then it moves to the parent directory, and so on, until the root of the tree is reached.

* `/home/ry/projects/node_modules/bar.js`
* `/home/ry/node_modules/bar.js`
* `/home/node_modules/bar.js`
* `/node_modules/bar.js`

This allows programs to localize their dependencies, so that they do not clash.

----

#### Using this modules loading pattern means npm will install this:

```bash
├── log4net@1.2.10.0
└─┬ some-other-module@1.2.3
  └── log4net@1.2.12.0
```

#### Into this folder structure:

```bash
├─┬ node_modules/
│ ├── log4net/             (v1.2.10.0)
│ └─┬ some-other-module/   (V1.2.3)
│   └─┬ node_modules/
│     └── log4net/         (v1.2.12.0)
└── your-project...
```

----

![](/check-yourself.jpg)

----

### The dependency hell will heat up when you start building plugins

```bash
├── gulp@3.8.7
├─┬ gulp-cjsx@0.3.0
│ ├── gulp@3.6.0
│ └── gulp-util@2.2.20
└── gulp-util@3.0.0
```

----

### Peer Dependencies

A dependency which also must be a dependency of the requesting module. This will ensure that the same version of the dependency is used as that of the downstream module.

----

### With Peer Dependencies this:

```bash
├── gulp@3.8.7
├─┬ gulp-cjsx@0.3.0
│ ├── gulp@3.6.0
│ └── gulp-util@2.2.20
└── gulp-util@3.0.0
```

### Becomes this:

```bash
├── gulp@3.8.7
├─┬ gulp-cjsx@0.3.0
│ ├── gulp@3.8.7
│ └── gulp-util@3.0.0
└── gulp-util@3.0.0
```

----

### And would install to:

```bash
├─┬ node_modules/
│ ├── gulp/                (v3.8.7)
│ ├── gulp-cjsx/           (v0.3.0)
│ └── gulp-util/           (v3.0.0)
└── your-project...
```

----

### Development Dependencies

Required only when you do development activities for your module. IE Testing or Building

----

## Versioning with SemVer

Given a version number MAJOR.MINOR.PATCH, increment:
<br />
<br />

<ul>
  <li class='fragment'>MAJOR version when make incompatible API changes</li>
  <li class='fragment'>MINOR version when you add functionality in a backwards-compatible manner</li>
  <li class='fragment'>PATCH version when you make backwards-compatible bug fixes</li>
</ul>

<br />

<p class='fragment'>
  <i>Additional labels for pre-release and build metadata are available as extensions to the MAJOR.MINOR.PATCH format.</i>
</p>

----

### Version Specifiers

* `1.2.3` - Specific version, includes `1.2.3+build2012`
* `>1.2.3`
* `<1.2.3`
* `>=1.2.3` - Does not include 1.2.3-beta
* `<=1.2.3` - Does include 1.2.3-beta
* `1.2.3 - 2.3.4` - Same as `>=1.2.3 <=2.3.4`
* `~1.2.3` - Same as `>=1.2.3-0 <1.3.0-0`
* `^1.2.3` - Same as `>=1.2.3-0 <2.0.0-0`
* `~1.2` - Same as `>=1.2.0-0 <1.3.0-0`
* `^1.2` - Same as `>=1.2.0-0 <2.0.0-0`

----

### Outdated dependencies

```bash
$ npm outdated
$ npm update
```

----

### Napa

Useful to acquiring dependencies that are not published to the registry, but does exist in an accessible git repo.

```json
  ...
  "scripts": {
    "postinstall": "napa"
  },
  "napa": {
    "jQueryMask": "igorescobar/jQuery-Mask-Plugin"
  },
  "devDependencies": {
    "napa": "^0.4.1",
  },
  ...
```

---

### Packaging/Publishing your module

----

### What exactly is included in your module?

<br />

<p class='fragment'>
  All files in you project
</p>

<br />

<p class='fragment'>
  Unless you have a `.gitignore` or an `.npmignore` file at the root of your project
</p>

----

### Prepublish Script

#### Highly recommended when your _"install"_ is not dependent on the operating system or architecture of the target system

<ul class='fragment'>
  <li>Compile CoffeeScript to JavaScript</li>
  <li>Create minified versions of JavaScript</li>
  <li>Fetching remote resources used by your package</li>
</ul>

----

### Napa and publishing your scripts

Napa is great at getting you packages not in the registry, but downstream project would not see this dependency. Make sure you include the content is some fashion.

```json
  ...
  "scripts": {
    "postinstall": "napa"
  },
  "napa": {
    "jQueryMask": "igorescobar/jQuery-Mask-Plugin"
  },
  "devDependencies": {
    "napa": "^0.4.1"
  },
  ...
```

----

### Deprication

```bash
$ npm depricate package-name@"< 0.2.3" "critical bug has been fixed in 0.2.3"
```

This command will update the npm registry entry for a package with a deprecation warning to all who attempt to install it.

<p class='fragment'>To un-depricate a package specify an empty string for the message argument.</p>

----

### Unpublish

```bash
$ npm unpublish <name>[@<version>]
```

__It is considered bad behavior to remove versions of a library that others are depending on!__

---

### Development Experience

```bash
$ npm link
```

---

### Enterprise NPM (Beta)

* Internal private modules
* Better control of development and deployment workflow
* Stricter security around deploying open-source modules
* Compliance with legal requirements to host code on-premise

----

### Eliminate conflicts with public modules

If a modules already exists in the public registry then you cannot create it in you internal registry

This also applies to internal modules, the name will be reserved in the public registry to prevent conflicts

----

### Selective Mirror

You can cache public modules within your network

----

### Privately scoped modules

```bash
$ npm login --registry=http://node-registry.towerswatson.com --scope=tw
$ npm install @tw/somepackage
```

#### In JavaScript

```javascript
require('@tw/somepackage');
```

#### In Package.Json

```json
{
  "name": "@tw/package-name"
  ...
}
```

_npm will automatically publish to your npmE, and will refuse to publish scoped packages to the public registry_

---

## Questions?
