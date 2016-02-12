var $ = require('jquery');
var ko = require('knockout');

function AppViewModel() {
    this.species = ko.observableArray();
    this.natures = ko.observableArray();
    this.pokemon = ko.observable({Nature: null, Level: null});
}

function normaliseDexNumber(dexNum) {
    var s = String(dexNum);
    while (s.length < 3) {
        s = '0' + s;
    }
    return s;
}

var appViewModel = new AppViewModel();

ko.applyBindings(appViewModel);

$.getJSON("http://localhost:25072/api/v0/Natures", function (data) {
    appViewModel.natures(data);
});

$.getJSON("http://localhost:20640/api/v0/Species", function (data) {
    appViewModel.species(data);
});

$.getJSON("http://localhost:57528/api/v0/Pokemon", function (data) {
    appViewModel.pokemon(data);
});