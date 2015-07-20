function MovieCtrl($scope) {
	$scope.movies = ["Veronica Mars", "Ice Age 3", "Final Destination"];
	$scope.addMovie = function (movie, $http) {

		$scope.movies.push(movie);
		var url = "/Movie/AddMovie";
		//$http.get(url)
		//	.success(function(data) {
		//	$scope.movies = data.statuses;
		//});
	}
}
