angular.module('myapp').run(['$templateCache', function ($templateCache) {
$templateCache.put('template1.html', "test");
}]);