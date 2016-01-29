var $ = require('jquery');

$.getJSON("http://localhost:25072/api/v0/Natures", function(data) {
    console.log(data);
});