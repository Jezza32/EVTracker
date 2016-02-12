var $ = require('jquery');
var ko = require('knockout');

function AppViewModel() {
    this.allSpecies = ko.observableArray();
    this.species = ko.observable();
    this.allNatures = ko.observableArray();
    this.nature = ko.observable();
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
    appViewModel.allNatures(data);
});

$.getJSON("http://localhost:20640/api/v0/Species", function (data) {
    appViewModel.allSpecies(data);
});