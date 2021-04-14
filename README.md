# Hugo / Azure Static Web Apps / Netlify CMS quickstart template

Create a static website with [Hugo](https://gohugo.io/), [Azure Static Web Apps](https://docs.microsoft.com/en-us/azure/static-web-apps/) and optionally [Netlify CMS](https://www.netlifycms.org/). For an example see [www.how2code.info](https://www.how2code.info).

> Static websites ([Jamstack sites](https://www.jamstack.org)) have many benefits, including better performance, higher security and lower costs. Read [this article](https://www.how2code.info/en/blog/azure-static-web-apps-the-fast-and-secure-way-to-run-your-blog/) for an introduction to the Jamstack architecture.

*Hugo* is a static website generator that has hundreds of free [themes](https://themes.gohugo.io/) available. This quickstart template uses the [Clarity](https://themes.gohugo.io/hugo-clarity/) theme. Thanks Chip Zoller and Dan Weru for your theme!

*Azure Static Web Apps* is a feature-rich hosting service for static web apps. It offers custom domains, CDN, automatic certificates, API hosting, easy CI/CD setup, and many more benefits.

*Netlify CMS* is a headless CMS (a content editor) that is compatible with most static website generators. It lives and stores content in your website GitHub repository. Setup is very easy.

# Instructions

## 1. Create your repository
Create your own copy of this repository. Visit [this link](https://github.com/jahlen/hugo-azure-static-webapp-quickstart/generate) to create.

## 2. Create a Static Web App in the Azure Portal
In the [Azure Portal](https://portal.azure.com/), search for Static Web App and click Create. 

Sign in with your GitHub account. Replace the yellow marked fields below with your own values:

![Create Static Web App](readme-images/static-webapp-create.png)

Enter the following locations:

![Create Static Web App](readme-images/static-webapp-create-2.png)

Your Azure Static Web App should be created within a few minutes.

## 3. Check your GitHub action

Azure should have created a GitHub action in your repository. It should be found under *.github/workflows*. Check that it exists. Otherwise something went wrong during step 2.

## 4. (Optional) Add a custom domain in Azure

*Only if you already have a domain that you wish to use, for example www.mydomain.com*. 

In the Azure Portal / Static Web App, go to **Custom domains** under Settings and add it. Follow the instructions for configuring your DNS-server.

Wait a couple of minutes for your custom domain to be completely setup, including the HTTP certificate.

## 5. Edit configuration files in your repo

In your GitHub repository, edit the following files:

* app/config.toml
* app/config/_default/config.toml
* app/config/_default/params.toml

Make sure to point **baseurl** to your website address. This could either be your custom domain, or the website address you were given by Azure Static Web Apps.

## 6. Visit your website

Now visit your website! Try clicking around on the website to check that it works. If not, check out the configuration files from step 5.

## 7. Configure Netlify CMS

There is an Edit-button at the bottom of your website. It takes you to Netlify CMS.

You will need to setup GitHub authentication for Netlify CMS to work.
