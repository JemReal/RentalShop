/*
 * This file launches the application by asking Ext JS to create
 * and launch() the Application class.
 */
Ext.application({
    extend: 'RentalShopUI.Application',

    name: 'RentalShopUI',

    requires: [
        // This will automatically load all classes in the RentalShopUI namespace
        // so that application classes do not need to require each other.
        'RentalShopUI.*'
    ],

    // The name of the initial view to create.
    mainView: 'RentalShopUI.view.main.Main'
});
