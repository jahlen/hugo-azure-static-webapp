+++
author = "How2Code.info"
title = "Welcome to your new static website"
date = "2021-04-15"
description = "An introduction to your new website"
featured = true
categories = [
    "getting started"
]
tags = [
    "setup"
]
thumbnail = "images/building.png"

+++

Congratulations! If you see this, your new static website is up and running. This quickstart template includes [Hugo](https://gohugo.io/), [Azure Static Web Apps](https://docs.microsoft.com/en-us/azure/static-web-apps/) and [Netlify CMS](https://www.netlifycms.org/).

* *Hugo* is a static website generator that has hundreds of free [themes](https://themes.gohugo.io/) available. This quickstart template uses the [Clarity](https://themes.gohugo.io/hugo-clarity/) theme. Thanks Chip Zoller and Dan Weru for your theme!
* *Azure Static Web Apps* is a feature-rich hosting service for static web apps. It offers custom domains, CDN, automatic certificates, API hosting, easy CI/CD setup, and many more benefits.
* *Netlify CMS* is a headless CMS (a content editor) that is compatible with most static website generators. It lives and stores content in your website GitHub repository. Setup is very easy.

Static websites ([Jamstack sites](https://www.jamstack.org)) have many benefits, including better performance, higher security and lower costs. Read [this article](https://www.how2code.info/en/blog/azure-static-web-apps-the-fast-and-secure-way-to-run-your-blog/) for an introduction to the Jamstack architecture.

## Instructions

You will find the latest instructions on how to setup and configure your static website [here](https://github.com/jahlen/hugo-azure-static-webapp).

#### How to edit your website

Your website comes with the Hugo Clarity theme. Here are [instructions](https://github.com/chipzoller/hugo-clarity) for configuring the theme.

You can edit the files under *app/content*. Instead of editing them manually, you can use Netlify CMS. There is an Admin-button in the page footer that will take you to Netlify CMS.

#### How to setup Netlify CMS

You need to update *app/static/admin/config.yml* and setup OAuth authentication. Check the instructions [here](https://github.com/jahlen/hugo-azure-static-webapp#8-optional-configure-netlify-cms).

#### Links not working?
Check the baseurl setting in your *app/config/_default/config.toml* file.

#### How to change title?
Change it in the *app/config/_default/languages.toml* file.

#### How to change author?
Change it in the *app/config/_default/params.toml* file.

#### Change Hugo Theme?
Browse for [themes](https://themes.gohugo.io/) and follow their setup instructions.
