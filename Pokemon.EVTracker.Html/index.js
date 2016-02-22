var $ = require('jquery');
var ko = require('knockout');

function AppViewModel() {
    this.stats = ko.observableArray(["HP", "Attack", "Defence", "SpecialAttack", "SpecialDefence", "Speed"]);
    this.selectedRoute = ko.observable({ Name: null, Pokemon: null });
    this.selectedGame = ko.observable({ Routes: [{ Name: null, Pokemon: null }] });
    this.games = ko.observableArray([{ Routes: [] }]);
    this.species = ko.observableArray();
    this.natures = ko.observableArray();
    this.heldItems = ko.observableArray();
    this.pokemon = ko.observable({ Nature: null, Level: null, HeldItem: null, IndividualValues: { HP: 0, Attack: 0, Defence: 0, SpecialAttack: 0, SpecialDefence: 0, Speed: 0 }, EffortValues: { HP: 0, Attack: 0, Defence: 0, SpecialAttack: 0, SpecialDefence: 0, Speed: 0 }, Stats: { HP: 0, Attack: 0, Defence: 0, SpecialAttack: 0, SpecialDefence: 0, Speed: 0 } });
}

function normaliseDexNumber(dexNum) {
    var s = String(dexNum);
    while (s.length < 3) {
        s = '0' + s;
    }
    return s;
}

var appViewModel = new AppViewModel();
var oldNature;

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

$.getJSON("http://localhost:39775/api/v0/Games", function (data) {
    appViewModel.games(data);
});

$(document).on("click", ".stat-change", function () {
    var type = $(this).data("type");
    var stat = $(this).data("stat");
    var change = $(this).data("change");
    $.getJSON("http://localhost:57528/api/v0/Pokemon/update/" + type + "/" + stat + "/" + change, function (data) {
        appViewModel.pokemon(data);
    });
});

$(document).on("click", ".defeat", function () {
    var dex = $(this).data("dex");
    $.getJSON("http://localhost:57528/api/v0/Pokemon/defeat/" + dex, function (data) {
        appViewModel.pokemon(data);
    });
});

$(document).on("change", "#nature-selector", function () {
    var nature = appViewModel.pokemon().Nature;
    if (nature != null && nature !== oldNature) {
        oldNature = nature;
        $.getJSON("http://localhost:57528/api/v0/Pokemon/update/nature/" + nature.Name, function (data) {
            appViewModel.pokemon(data);
        });
    }
});