# Welcome to AnalyticsTracker

**AnalyticsTracker** is a utility aimed at making it easier to track data from ASP.NET applications to Google Analytics and Google Tag Manager

- [Google Analytics](#google-analytics)
- [Google Tag Manager](#google-tag-manager)

## Installation

Installing AnalyticsTracker is simple. [Install it from NuGet](http://www.nuget.org/packages/AnalyticsTracker) to add it to your project.

## Google Analytics

In your template, add the following after the `<body>` to render the tracking script

### MVC

	@using Vertica.AnalyticsTracker
	...
	<body>
		@AnalyticsTracker.Render("UA-xxxxxx-1")
		...
	</body>

### Webforms

	<%@ Import Namespace="Vertica.AnalyticsTracker" %>
	...
	<body>
		<%= AnalyticsTracker.Render("UA-xxxxxx-1") %>
		...
	</body>

### Advanced tracker settings

You can tweak the settings of the tracker by using the overloads of the Render() method.

	@AnalyticsTracker.Render(
		account: "UA-xxxxxx-1",
		trackDefaultPageview: true,
		displayFeatures: false,
		trackerConfiguration: new Dictionary<string, object>
		{
			{"cookieDomain", "foo.example.com"},
			{"cookieName", "myNewName"},
			{"cookieExpires", 20000}
		});

### Ajax

AnalyticsTracker can also track data from ajax requests. We have included ajax interceptors for `jQuery` and `angular`, that you can include in you site. Make sure to include it after the frameworks.

	<script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
	<script src="//ajax.googleapis.com/ajax/libs/angularjs/1.3.12/angular.min.js"></script>
	<script src="/Scripts/analyticstracker.adapters.js"></script>

### Usage

AnalyticsTracker aims to provide som sensible defaults, and let you override some of the advanced settings if you need to.

#### Override url

If you want to track a different url for the pageview than the one in the browser, you can override it by setting the page of the current tracker

	AnalyticsTracker.Current.SetPage("/my/custom/url");

#### Commands

By default AnalyticsTracker will output the basic tracking code, and track a standard pageview. When you want to track additional data, you add `commands` to the current tracker. 

Here we are tracking an event from a controller

	public ActionResult About()
	{
		AnalyticsTracker.Current.Track(new EventCommand("category", "action", "label"));
		ViewBag.Message = "Your application description page.";
		return View();
	}


#### Ecommerce tracking

The command to track an ecommerce transaction can be used like this:

	var transaction = new TransactionInfo(
		id: "order1001", 
		affiliation: string.Empty, 
		revenue: 100, 
		shipping: 10, 
		tax: 5, 
		currency: AnalyticsCurrency.USD);

	transaction.AddItem(
		name: "Black shirt", 
		sku: "sh001", 
		category: "Shirts", 
		price: 50, 
		quantity: 2);

	AnalyticsTracker.Current.Track(new TransactionCommand(transaction));


#### Enhanced Ecommerce tracking

If you have enabled Enhanced Ecommerce tracking in your account, then you should use those commands to track your transactions:

	var purchaseAction = new PurchaseActionFieldObject(
		id: "order1001",
		affiliation: string.Empty,
		revenue: 100,
		shipping: 10,
		tax: 5,
		coupon: null);

	var lineItems = new []
	{
		new ProductFieldObject(
			id: "sh001", 
			name: "Black shirt", 
			brand: "Northwind",
			category: "Shirts", 
			variant: null, 
			price: 50,
			quantity: 2, 
			coupon: null,
			position: 1)
	};
	
	AnalyticsTracker.Current.Track(new PurchaseCommand(purchaseAction, lineItems));

You may also want to set the currency on your tracker when tracking ecommerce

Enhanced Ecommerce also lets you track a lot of other things. The commands are located in the `Vertica.AnalyticsTracker.Commands.EnhancedEcommerce` namespace:

	AddToBasketCommand(...)
	RemoveFromBasketCommand(...)
	CheckoutCommand(...)
	CheckoutOptionCommand(...)
	ProductClickCommand(...)
	ProductDetailCommand(...)
	ProductListCommand(...)

#### Track only once
A typical thing you want to do is to make sure that for example transactions are only tracked once, even if the user reloads the page. You should keep track of this from your server, but there will typically also be the issue of users using the back button, or browsers rehydrating a page etc. where there is no actual server interaction. 

Enter the `CookieGuardedCommand`:

	AnalyticsTracker.Current.Track(new CookieGuardedCommand(innerCommand, "order12345"));

Wrapping your command in a `CookieGuardedCommand` will render the inner command inside an `if` statement like this:

	if (document.cookie.search(/AnalyticsTrackerGuardorder12345=true/) === -1) {
		ga('send', {'hitType': 'event','eventCategory': 'cat','eventAction': 'act','eventLabel': ''});
		document.cookie = 'AnalyticsTrackerGuardorder12345=true; Expires=' + new Date(2016, 05, 02).toUTCString();
	}

This will ensure that the command is not executed again, if the same javascript is executed twice. User for example the order id as the command id to make sure that the cookie is only set for that specific order.

#### Client side tracking

Sometimes you have events happening without any server interaction. For example clicking on an image to zoom it. In this case you can use AnalitycsTracker to generate the needed script:


	<a href="#" onmousedown="@AnalyticsTracker.ClientCommand(new EventCommand("category", "action", "label"))">
		<img src="..."/>
	</a>

## Google Tag Manager

In your template, add the following after the `<body>` to render the tracking script

### MVC

	@using Vertica.AnalyticsTracker
	...
	<body>
		@TagManager.Render("GTM-XXYYY")
		...
	</body>

### Webforms

	<%@ Import Namespace="Vertica.AnalyticsTracker" %>
	...
	<body>
		<%= TagManager.Render("GTM-XXYYY") %>
		...
	</body>

### Advanced tracker settings

You can tweak the settings of the tag by using the overloads of the Render() method.

	@TagManager.Render(account:"GTM-XXYYY", dataLayerName: "myLayer");

### Usage

Using the TagManager lets you easily information to the `dataLayer` variable.

	TagManager.Current.AddMessage(new Variable("myVariable", "myValue"));

This will generate the following dataLayer

	<script>
	var dataLayer = [];
	dataLayer.push({'myVariable': 'myValue'});
	</script>

#### Complex variables

Sometimes you might want to add more complex objects to the dataLayer, and this is supported by using a dictionary

	var dict = new Dictionary<string, object>
	{
		{"myProp", "myValue"},
		{"yourProp", 12345}
	};
	TagManager.Current.AddMessage(new Variable("myVariable", dict));
 
The dictionary will then be rendered as a deep object

	<script>
	var dataLayer = [];
	dataLayer.push({'myVariable': {'myProp': 'myValue','yourProp': 12345}});
	</script>

#### Ecommerce order tracking

Using the TransactionMessage, order information can be sent through the dataLayer:

	var items = new[]
	{
		new TransactionItemInfo(
			name: "Black shirt",
			sku: "sh001",
			category: "Shirts",
			price: 50,
			quantity: 2)
	};

	var message = new TransactionMessage(
		transactionId: "order1001",
		affiliation: string.Empty,
		total: 100,
		tax: 5,
		shipping: 10,
		items: items);
	
	TagManager.Current.AddMessage(message);

The order will be rendered according to the GTM specifications:

	<script>
	var dataLayer = [];
	dataLayer.push(
		{
			'transactionId': 'order1001',
			'transactionAffiliation': '',
			'transactionTotal': 100,
			'transactionTax': 5,
			'transactionShipping': 10,
			'transactionProducts': [
				{
					'name': 'Black shirt',
					'sku': 'sh001',
					'category': 'Shirts',
					'price': 50,
					'quantity': 2
				}
			]
		});
	</script>
