angular.module("eventDemo", ['signalR.eventAggregator', 'ngGrid'])
	.controller("EventController", [
	"$scope", "$http", "$interval", function ($scope, $http, $interval) {
	    $scope.phoneNumber = null;
	    $scope.agentId = null;
	    $scope.events = [];
	    $scope.Subscribed = false;
	    $scope.Calling = false;
	    $scope.CallAnswered = false;
	    $scope.CallEnded = false;
	    $scope.AutoDialling = false;
	    $scope.AutoDiallingWithNoCounter = false;

	    // Counter
	    $scope.counter = 0;
	    $scope.initialCountdown = 5;
	    $scope.countdown = $scope.initialCountdown;
	    var intervalId;

	    $scope.SlotData = [];

	    $scope.gridOptions = { data: "SlotData" };
	   
	    $scope.timer = function () {
	        var startTime = new Date();
	        intervalId = $interval(function () {
	            var actualTime = new Date();
	            $scope.counter = Math.floor((actualTime - startTime) / 1000);
	            $scope.countdown = $scope.initialCountdown - $scope.counter;
	        }, 1000);
	    };

	    $scope.$watch('countdown', function (countdown) {
	        if (countdown === 0) {
	            $scope.stop();
	            $scope.StartAutoDialling();
	        }
	    });

	    $scope.start = function () {
	        $scope.timer();
	    };

	    $scope.stop = function () {
	        $interval.cancel(intervalId);
	    };

	    $scope.canFire = function () {
	        return $scope.phoneNumber != null && $scope.phoneNumber.trim() != "";
	    };

	    $scope.canEndCall = function () {
	        if ($scope.Subscribed) {
	            if ($scope.Calling || $scope.CallAnswered) {
	                return true;
	            }
	        }
	        else {
	            return false;
	        }
	    };

	    $scope.canCallNumber = function () {
	        if ($scope.Subscribed) {
	            if ($scope.Calling || $scope.CallAnswered) {
	                return false;
	            }
	            else {
	                return true;
	            }
	        }
	        else {
	            return false;
	        }
	    }

	    $scope.StartAutoDiallingWithNoCounter = function () {
	        $scope.AutoDiallingWithNoCounter = true;
	        $scope.StartAutoDialling();
	    }

	    $scope.StartAutoDialling = function () {
	        if ($scope.gridOptions.ngGrid.config.selectedItems.length > 0) {
	            $scope.AutoDialling = true;
	            callNumberAuto($scope.gridOptions.ngGrid.config.selectedItems[0].Phone);
	        } else {
	           $scope.StopAutoDialling();
	        }
	    };

	    $scope.StopAutoDialling = function () {
	        $scope.AutoDialling = false;
	        $interval.cancel(intervalId);
	    };

	    $scope.PublishEvent = function () {
	        post("http://localhost:58105/api/service/PublishEvent", $scope.phoneNumber);
	    };

	    $scope.ConstrainedEvent = function () {
	        post("http://localhost:58105/api/service/ConstrainedEvent", $scope.phoneNumber);
	    };

	    $scope.DiallerEventPublish = function () {
	        post("http://localhost:58105/api/service/DiallerEvent", $scope.agentId);
	    };

	    $scope.callNumber = function () {
	        $scope.events = [];
	        var adUserName = 'shashmi';
	        var site = 'MK';
	        var agentId = $scope.agentId;
	        var phoneNumber = $scope.phoneNumber;
	        var prospectId = 1234;
	        var connObj = { 'domainUserName': adUserName, 'site': site, 'phoneNumber': phoneNumber, 'prospectId': prospectId, 'agentId': agentId };
	        get("http://localhost:58105/api/service/DialNumber", connObj);
	    };

	    function callNumberAuto (phoneNumber) {
	        $scope.events = [];
	        var adUserName = 'shashmi';
	        var site = 'MK';
	        var agentId = $scope.agentId;
	        var phoneNumber = phoneNumber;
	        var prospectId = 1234;
	        var connObj = { 'domainUserName': adUserName, 'site': site, 'phoneNumber': phoneNumber, 'prospectId': prospectId, 'agentId': agentId };
	        get("http://localhost:58105/api/service/DialNumber", connObj);
	    };

	    $scope.Start = function () {
	        LoadData();
	        var agentId = $scope.agentId;
	        $scope.eventAggregator().subscribe(SignalR.Event.Contract.Events.DiallerEvent, onEvent, { agentId: agentId });
	        $scope.Subscribed = true;
	        $scope.gridOptions.selectAll(true);
	    };

	    $scope.Stop = function () {
	        $scope.eventAggregator().unsubscribe(this);
	        $scope.Subscribed = true;
	    };


	    $scope.EndCall = function () {
	        var agentId = $scope.agentId;
	        post("http://localhost:58105/api/service/EndCall", agentId);
	    };

	    function AutoDialCallDisconnected() {
	        if ($scope.gridOptions.ngGrid.config.selectedItems.length > 0) {
	            $scope.unSelectRow($scope.gridOptions.ngGrid.config.selectedItems[0].Name);
	        }
	    };

	    $scope.unSelectRow = function (ProspectName) {
	        angular.forEach($scope.SlotData, function (data, index) {
	            if (data.Name == ProspectName) {
	                $scope.gridOptions.selectItem(index, false);
	            }
	        });
	    };


	    $scope.GetEvent = function () {
	        var url = 'http://localhost:58105/api/';
	        var promise = $http.get(url + 'service/GetEvent');
	    };

	    $scope.SelectAll = function () {
	        $scope.gridOptions.selectAll(true);
	    };

	    function post(url, data) {
	        $http.post(url, angular.toJson(data));
	    };

	    function get(url, data) {
	        $http.get(url, { params: data, withCredentials: false });
	    };

	    function onEvent(e) {
	        $scope.events.push(e);
	        SetEvent(e.Message);
	    };

	    function SetEvent(message) {
	        if (message.indexOf("Initialised") > 0) {
	            $scope.Calling = true;
	            $scope.CallEnded = false;
	            $scope.CallAnswered = false;
	        } else if (message.indexOf("Disconnected") > 0) {
	            $scope.Calling = false;
	            $scope.CallEnded = true;
	            $scope.CallAnswered = false;
	            if ($scope.AutoDiallingWithNoCounter) {
                    AutoDialCallDisconnected();
                    $scope.StartAutoDiallingWithNoCounter();
	            } else if ($scope.AutoDialling) {
	                AutoDialCallDisconnected();
	                $scope.start();
	            }
	        } else if (message.indexOf("Answered") > 0) {
	            $scope.Calling = false;
	            $scope.CallEnded = false;
	            $scope.CallAnswered = true;
	        }
	    };

	    function LoadData() {
            if($scope.numbertoLoad != null)
            {
                $scope.SlotData = [{ Name: "Mr A", Phone: $scope.numbertoLoad },
                         { Name: "Mr B", Phone: $scope.numbertoLoad },
                         { Name: "Mr C", Phone: $scope.numbertoLoad },
                         { Name: "Mr D", Phone: $scope.numbertoLoad },
                         { Name: "Mr E", Phone: $scope.numbertoLoad }];
                $scope.gridOptions.selectAll(true);
            } else if ($scope.agentId != '1035') {
	            $scope.SlotData = [{ Name: "Mr A", Phone: '1033' },
                         { Name: "Mr B", Phone: '1033' },
                         { Name: "Mr C", Phone: '1033' },
                         { Name: "Mr D", Phone: '1033' },
                         { Name: "Mr E", Phone: '1033' }];
	            $scope.gridOptions.selectAll(true);

	        } else {
	            $scope.SlotData = [{ Name: "Mr A", Phone: '07809230676' },
                   { Name: "Mr B", Phone: '07809230676' },
                   { Name: "Mr C", Phone: '07809230676' },
                   { Name: "Mr D", Phone: '07809230676' },
                   { Name: "Mr E", Phone: '07809230676' }];
	            $scope.gridOptions.selectAll(true);
	        }
	    };

	}
	]);