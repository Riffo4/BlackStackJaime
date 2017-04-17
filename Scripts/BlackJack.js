$(document).ready(function () {
    console.log("Success i document ready"); 
});

function newGame() {
    location.reload();
}

function SetupGame() {
    var data = {
        name: $('#playerName')[0].value
    };

    $.ajax({
        url: '../Home/BlackJack',
        contentType: 'application/json; charser=utf=8',
        datatype: 'json',
        method: 'POST',
        data: JSON.stringify(data),
        success: function (game) {
            $(".blackJackGame").html(game);
            console.log("Game setup success");
        }
    });
}

function updateView() {
    $('#playControl').css('visibility', 'visible');

    $('.betingControl').prop('disabled', true);
    $('.betingControlImg').addClass('hidden');
}

function hit() {    
    if (currentGame.game.Bet == 0) {
        alert('You need to bet to start playing');
        return;
    }

    var data = {
        bet: currentGame.game.Bet
    };

    $.ajax({
        url: '../Home/Hit',
        contentType: 'application/json; charser=utf=8',
        datatype: 'json',
        method: 'POST',
        data: JSON.stringify(data),
        success: function (game) {

            $(".blackJackGame").html(game);
            console.log("Hit success");

            $('#playControl').css('visibility', 'visible');

            if (currentGame.game.Result) {
                $('#playControl').css('visibility', 'hidden');
                $('#dealbutton').html('Redeal');
                $('.betingControlImg').addClass('hidden');
                $('.betingControl').prop('disabled', true);

                $('#dealbutton').prop('disabled', false);

                if (!currentGame.game.Result.PlayerWin && currentGame.game.Player.Money < 1) {
                    $('#dealbutton').html('New game');
                }
            }
        }
    });
}


function stand() {
    $.ajax({
        url: '../Home/Stand',
        contentType: 'application/json; charser=utf=8',
        datatype: 'json',
        method: 'POST',
        success: function (game) {

            $(".blackJackGame").html(game);
            console.log("Stand success");

            if (currentGame.game.Result) {
                $('#playControl').css('visibility', 'hidden');
                $('#dealbutton').html('Redeal');
                $('.betingControlImg').addClass('hidden');
                $('.betingControl').prop('disabled', true);

                $('#dealbutton').prop('disabled', false);

                if (!currentGame.game.Result.PlayerWin && currentGame.game.Player.Money < 1) {
                    $('#dealbutton').html('New game');
                }
            }
            //$('#playControl').removeClass('hidden');
        }
    });
}


function StartGame() {
    var dealOrRedeal = $('#dealbutton')[0].innerText;
    if (dealOrRedeal == 'Deal') {
        if (currentGame.game.Bet == 0) {
            alert('You need to bet to start playing');
            return;
        }

        var data = {
            bet: currentGame.game.Bet
        };

        $.ajax({
            url: '../Home/StartGame',
            contentType: 'application/json; charser=utf=8',
            datatype: 'json',
            method: 'POST',
            data: JSON.stringify(data),
            success: function (game) {
                $(".blackJackGame").html(game);
                console.log("Success game started");

                updateView();
            }
        });
    }
    else {
        $.ajax({
            url: '../Home/NewRound',
            contentType: 'application/json; charser=utf=8',
            datatype: 'json',
            method: 'POST',
            success: function (game) {

                $(".blackJackGame").html(game);
                console.log("Hit success");
            }
        });
    }
}

function ffat20() {
    $.ajax({
        url: '../Home/Surrender',
        contentType: 'application/json; charser=utf=8',
        datatype: 'json',
        method: 'POST',
        success: function (game) {
            $(".blackJackGame").html(game);
            console.log("Surrendered");

            $('#playControl').css('visibility', 'hidden');
            $('#dealbutton').html('Redeal');
            $('.betingControlImg').addClass('hidden');
            $('.betingControl').prop('disabled', true);

            $('#dealbutton').prop('disabled', false);

            if (!currentGame.game.Result.PlayerWin) {
                $('#dealbutton').html('New game');
            }
        }
    });
}

function clearBet() {
    $.ajax({
        url: '../Home/ClearBet',
        contentType: 'application/json; charser=utf=8',
        datatype: 'json',
        method: 'POST',
        success: function (game) {
            $(".blackJackGame").html(game);
            console.log("Cleared bet amount");
        }
    });
}

function add5() {
    var data = {
        amount: 5
    };

    callAddbet(data);
}

function add10() {
    var data = {
        amount: 10
    };

    callAddbet(data);
}

function add25() {
    var data = {
        amount: 25
    };

    callAddbet(data);
}

function add50() {
    var data = {
        amount: 50
    };

    callAddbet(data);
}

function add100() {
    var data = {
        amount: 100
    };

    callAddbet(data);
}

function callAddbet(data) {
    $.ajax({
        url: '../Home/AddBet',
        contentType: 'application/json; charser=utf=8',
        datatype: 'json',
        method: 'POST',
        data: JSON.stringify(data),
        success: function (game) {
            $(".blackJackGame").html(game);
            console.log("Added " + data.amount + "to bet");
        }
    });
}