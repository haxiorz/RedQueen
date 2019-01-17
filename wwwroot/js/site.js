// Write your JavaScript code.
$('.newCoinForm').submit(function (e) {
    GetCoinName();
});

function GetCoinName() {
    var coinName = $('.filter-option.pull-left').text();
    $('.NewCoinName').val(coinName);
}