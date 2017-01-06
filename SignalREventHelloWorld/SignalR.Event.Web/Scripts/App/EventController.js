angular.module("eventDemo", ['signalR.eventAggregator'])
	.controller("EventController", [
	"$scope", "$http",  function ($scope, $http) {
	    $scope.events = [];

	    $scope.PublishEvent = function () {
	        post("http://localhost:58105/api/service/PublishEvent", $scope.name);
	    };

	    $scope.ConstrainedEvent = function () {
	        post("http://localhost:58105/api/service/ConstrainedEvent", $scope.name);
	    };
        
	    $scope.GetEvent = function () {
	        var url = 'http://localhost:58105/api/';
	        var promise = $http.get(url + 'service/GetEvent');
	    };

	    function post(url, data) {
	        $http.post(url, angular.toJson(data));
	    };

	    function get(url, data) {
	        $http.get(url, { params: data, withCredentials: false });
	    };

	    function onEvent(e) {
	        $scope.events.push(e);
	    };

	    $scope.eventAggregator().subscribe(SignalR.Event.Contract.Events.GenericEvent.of("System.String"), onEvent, this);
	}
	]);