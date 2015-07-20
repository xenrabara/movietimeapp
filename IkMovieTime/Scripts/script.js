function MovieCtrl($scope) {
	$scope.movies = [];
	$scope.addMovie = function (movie, $http) {
		$scope.movies.push(movie);
	}
}

function TeamCtrl($scope, $http) {
	var getTeamUrl = "/API/Teams/";
	$scope.team = null;

	//Get Employees Per Team
	$http.get(getTeamUrl)
		.success(function (teams) {
			$scope.teams = teams;
			$scope.team = teams[0];

			getEmployeesPerTeam($http, $scope, $scope.team.TeamID);
		});

	//Get Available Employees
	getAvailablePlayers($scope, $http);

	$scope.teamSelected = function (selectedTeam) {
		$scope.team = selectedTeam;
		getEmployeesPerTeam($http, $scope, selectedTeam.TeamID);
	};

	$scope.assignEmployeeToTeam = function (availableEmployeeID) {
		var addToTeamUrl = "/API/Employees/AddRemoveEmployeeFromTeam";
		var data = {
			EmployeeId: availableEmployeeID,
			TeamId: $scope.team.TeamID

		};
		$http.post(addToTeamUrl, data)
			.success(function () {
				updateEmployeeLists($http, $scope, $scope.team.TeamID);
			}
		);
	}

	$scope.removeEmployeeFromTeam = function (employeeInTeamID) {
		var removeFromTeamUrl = "/API/Employees/AddRemoveEmployeeFromTeam";
		var data = {
			EmployeeId: employeeInTeamID,
			TeamId: null,

		};
		$http.post(removeFromTeamUrl, data)
			.success(function () {
				updateEmployeeLists($http, $scope, $scope.team.TeamID);
			}
		);
	}
}

function getEmployeesPerTeam($http, $scope, teamId) {
	$http.get("/API/Employees/GetEmployeesPerTeam/" + teamId)
			.success(function (employees) {
				$scope.employeesPerTeam = employees;
				$scope.employee = employees[0];
			});
}

function getAvailablePlayers($scope, $http) {
	$http.get("/API/Employees/GetEmployeesPerTeam/" + null)
			.success(function (availableEmployees) {
				$scope.availableEmployees = availableEmployees;
				$scope.availableEmployee = availableEmployees[0];

			});
}

function updateEmployeeLists($http, $scope, teamId) {
	getEmployeesPerTeam($http, $scope, teamId);
	getAvailablePlayers($scope, $http);
}