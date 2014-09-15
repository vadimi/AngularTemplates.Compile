angular.module('app', []).run(['$templateCache', function ($templateCache) {
$templateCache.put('template1.html', "test");
}]);