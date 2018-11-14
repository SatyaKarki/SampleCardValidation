(app.controller("homeCtrl", ["$rootScope", "$scope", "queryStringService", "httpService","$filter"
    , function ($rootScope
        , $scope
        , queryStringService, httpService, $filter) {

        $scope.Title = "Home Page";
        $scope.cardNumber = {};
        $scope.cardNumbers = [];
        $scope.insert = true;
        $scope.cardInputViewModel = {
            Active:true,
        };
        $scope.responseOutput = {};

        $scope.GetAllCard = function () {
            try {

                httpService.get("api/Card/GetAllCardNumbersUsingSP", null).then(
                    function success(response) {
                        if (response.data.success) {
                            $scope.cardNumbers = response.data.output;
                        }
                        else {
                            alert(response.data.message);
                        }

                    }, function error() {
                        alert("Failure response GetAllCard");
                    }
                );
            } catch (e) {
                alert('GetAllCard ' + e.message);
            }
        };
        $scope.OnInit = function () {
            $scope.GetAllCard();
        };


        $scope.ValidateCard = function () {
            try {
                var cardInputViewModel = $scope.cardInputViewModel;
                httpService.post("api/Card/CheckCard", cardInputViewModel).then(
                    function success(response) {
                        $scope.responseOutput = response.data;  
                    }, function error() {
                        alert("Something went wrong!");
                    }
                );
            } catch (e) {
                alert(e);
            }
        };
        $scope.ValidateCardBySP = function () {
            try {
                var cardInputViewModel = $scope.cardInputViewModel;
                httpService.post("api/Card/CheckCardUsingSP", cardInputViewModel).then(
                    function success(response) {
                        $scope.responseOutput = response.data;
                    }, function error() {
                        alert("Something went wrong!");
                    }
                );
            } catch (e) {
                alert(e);
            }
        };
        $scope.SaveCardBySP = function () {
            try {

                var Param = JSON.stringify($scope.cardInputViewModel);
                httpService.post("api/Card/SaveCardNumberBySP", Param).then(
                    function success(response) {
                        if (response.data.success) {
                            alert(response.data.message);
                            $scope.GetAllCard();
                            $scope.CancelCardNumber();
                        }
                        else {
                            alert(response.data.message);
                        }

                    }, function error() {
                        alert("failure response SaveCardBySP");
                    }
                );
            } catch (e) {
                alert('SaveCardBySP ' + e.message);
            }
        };

        $scope.EditCardNumber = function (item) {
           // $scope.cardInputViewModel = item;
            $scope.cardInputViewModel.cardNumber = item.CNumber;
            $scope.cardInputViewModel.expiryDate = item.Expiry;
            $scope.cardInputViewModel.Active = item.Active;
            $scope.insert = false;

        };

        $scope.SaveCard = function () {
            try {
                //var checkCard = $filter('filter')($scope.cardNumbers.CNumber, { CNumber: $scope.cardInputViewModel.cardNumber }, true)[0];
                //if (checkCard != null) {
                //    alert("This Card Already Exist in the system !!")
                //}
                var Param = JSON.stringify($scope.cardInputViewModel);
                httpService.post("api/Card/SaveCardNumber", Param).then(
                    function success(response) {
                        if (response.data.success) {
                            alert(response.data.message);
                            $scope.GetAllCard();
                            $scope.CancelCardNumber();
                        }
                        else {
                            alert(response.data.message);
                        }

                    }, function error() {
                        alert("failure response SaveCard");
                    }
                );
            } catch (e) {
                alert('SaveCard ' + e.message);
            }
        };


        $scope.CancelCardNumber = function () {
            try {
                $scope.cardInputViewModel = null;
                $scope.insert = true;
            } catch (e) {
                alert(e.message);
            }
        };

        $rootScope.replaceUrl = function (url, paramter, value) {
            var n = url.includes("?");
            if (n)
                return url + "&" + paramter + "=" + value;
            else
                return url + "?" + paramter + "=" + value;
        };

    }]));