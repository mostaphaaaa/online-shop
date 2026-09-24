function changeTake(value) {
    const url = new URL(window.location.href);

    url.searchParams.set('TakeEntity', value);
    url.searchParams.set('PageId', '1');

    window.location.href = url.toString();
}