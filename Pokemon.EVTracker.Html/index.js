var $ = require('jquery');
var ko = require('knockout');

function AppViewModel() {
    this.species = ko.observableArray();
    this.natures = ko.observableArray();
    this.heldItems = ko.observableArray();
    this.pokemon = ko.observable({ Nature: null, Level: null, HeldItem: null, IndividualValues: {HP:0, Attack:0, Defence:0,SpecialAttack:0,SpecialDefence:0,Speed:0 }, EffortValues: {HP:0, Attack:0, Defence:0,SpecialAttack:0,SpecialDefence:0,Speed:0 }, Stats: {HP:0, Attack:0, Defence:0,SpecialAttack:0,SpecialDefence:0,Speed:0 }});
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

$.getJSON("http://localhost:38327/api/v0/Items", function (data) {
    appViewModel.heldItems(data);
});