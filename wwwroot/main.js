
	var locked = false;
	
	function showInterface(type) {
		document.body.setAttribute('data-input', type);
	}
	
	function pushOutputRow (content, own) {
		var classname = own ? 'own' : 'fgn';
		terminal.innerHTML = terminal.innerHTML + '<div class="' + classname + '">' + content + '</div>';
	}
	
	function send(data) {
		if (locked)
			return;
		
		locked = true;
		
		socket.send(('' + data).trim());
	}

	function sendRoll() {
		send(sendRoll.currentValue.toString());
		sendRoll.rolling = false;
	}

	function sendCont() {
		send('c');
	}

	function sendDefault() {
		send(fi.innerText);
		
		pushOutputRow('<i class="cmd">>' + fi.innerText + '</i>', true);

		fi.innerHTML = '';
	}

	function setContinueText(val) {
		contButton.innerHTML = '>' + val + '<';
	}
	function setRollData(vals) {
		sendRoll.rolling = true;
		vals = vals.split(',');
		sendRoll.currentValue = 0;
		var l = vals.length;
		var t = 0;
		(function next() {
			setTimeout(function () {
				rollButton.innerHTML = 'ROLL ' + ('0' + vals[sendRoll.currentValue % l]).slice(-2);
				sendRoll.currentValue++;
				t++;
				sendRoll.currentValue = sendRoll.currentValue % l;
				
				if (t > 4 * 7 && sendRoll.rolling) {
					sendRoll();
				} else if (sendRoll.rolling) {
					next();
				}
			}, 250);
		})();
	}

	function updateStatsMain(data) {
		statsMain.innerHTML = data;
	}

	function updateStatsWeapon(data) {
		statsWeapon.innerHTML = data;
	}

	function updateStatsAttr(data) {
		statsAttr.innerHTML = data;
	}

	var progText = document.querySelector('.progText');
	var progAbort = document.querySelector('.progAbort');
	var progRestTime = document.querySelector('.progRestTime');
	var progSkipCosts = document.querySelector('.progSkipCosts');
	
	var rollButton = document.querySelector('.rollButton');
	var contButton = document.querySelector('.contButton');
	var terminal = document.querySelector('.terminal .scrollhelper');
	var statsMain = document.querySelector('.status .main');
	var statsWeapon = document.querySelector('.status .weapon');
	var statsAttr = document.querySelector('.status .attr');
	var fi = document.querySelector('.fakeinput');

	fi.contentEditable = 'true';
	fi.designMode = 'on';

	setTimeout(function () {
		fi.focus();
	}, 0);
	setTimeout(function () {
		fi.focus();
	}, 250);

	document.addEventListener('keydown', function (e) {
		if (e.key != 'Enter')
			return;

		switch (document.body.getAttribute('data-input')) {
			case 'cont':
				sendCont();
				break;
			
			case 'def':
				sendDefault();
				break;

			case 'roll':
				sendRoll();
				break;
		}
	});

	if ('serviceWorker' in navigator) {
		navigator.serviceWorker.register('worker.js')
			.then(function (reg) {
				console.log(reg);
			});
	}

	var socket = new WebSocket('ws://' + location.host + '/o/websocket');

	socket.onopen = function () {
		showInterface('def');
	};

	socket.onmessage = function (event) {
		if (event.data == '')
			return;

		var separator = '|';

		var payload = event.data.substring(event.data.indexOf(separator) + 1);
		var type = event.data.substring(0, event.data.indexOf(separator));

		switch (type) {
			case 'u':
				locked = false;
				break;
			
			case 'sm':
				updateStatsMain(payload);
				break;

			case 'sw':
				updateStatsWeapon(payload);
				break;
			
			case 'sa':
				updateStatsAttr(payload);
				break;

			case 'oo':
				pushOutputRow(payload, true);
				break;

			case 'of':
				pushOutputRow(payload, false);
				break;

			case 'i':
				showInterface('def');
				break;
			
			case 'p':
				var data = payload.split('|');
				
				progText.innerHTML = data[0];
				progRestTime.innerHTML = data[1];
				progSkipCosts.innerHTML = data[2];
				progAbort.style.display = data[3] == 'true' ? 'block' : 'none';
				
				showInterface('prog');
				break;

			case 'c':
				setContinueText(payload);
				showInterface('cont');
				break;

			case 'r':
				setRollData(payload);
				showInterface('roll');
				break;
		}

		switch (document.body.getAttribute('data-input')) {
			case 'def':
				fi.focus();
				break;
		}
	};