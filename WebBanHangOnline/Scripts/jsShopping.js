$(document).ready(function () {
    ShowCount();

    // Sự kiện click cho nút 'Add to Cart'
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = 1;
        var tQuantity = $('#quantity_value').text();
        if (tQuantity != '') {
            quantity = parseInt(tQuantity) || 1; // Xử lý trường hợp không phải số
        }

        $.ajax({
            url: '/shoppingcart/addtocart',
            type: 'POST',
            data: { id: id, quantity: quantity },
            success: function (rs) {
                if (rs && rs.Success) { // Kiểm tra nếu rs có giá trị
                    $('#checkout_items').html(rs.Count);
                    alert(rs.msg);
                }
            },
            error: function () {
                alert("Có lỗi xảy ra khi thêm vào giỏ hàng.");
            }
        });
    });

    // Sự kiện click cho nút 'Update'
    $('body').on('click', '.btnUpdate', function (e) {
        e.preventDefault();
        var id = $(this).data("id");
        var quantity = $('#Quantity_' + id).val();
        Update(id, quantity);
    });

    // Sự kiện click cho nút 'Delete All'
    $('body').on('click', '.btnDeleteAll', function (e) {
        e.preventDefault();
        var conf = confirm('Bạn có chắc muốn xóa hết sản phẩm trong giỏ hàng?');
        if (conf) {
            DeleteAll();
        }
    });

    // Sự kiện click cho nút 'Delete'
    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm('Bạn có chắc muốn xóa sản phẩm này khỏi giỏ hàng?');
        if (conf) {
            $.ajax({
                url: '/shoppingcart/Delete',
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    if (rs && rs.Success) { // Kiểm tra nếu rs có giá trị
                        $('#checkout_items').html(rs.Count);
                        $('#trow_' + id).remove();
                        LoadCart();
                    }
                },
                error: function () {
                    alert("Có lỗi xảy ra khi xóa sản phẩm.");
                }
            });
        }
    });
});

// Hàm hiển thị số lượng sản phẩm trong giỏ
function ShowCount() {
    $.ajax({
        url: '/shoppingcart/ShowCount',
        type: 'GET',
        success: function (rs) {
            if (rs) {
                $('#checkout_items').html(rs.Count);
            }
        },
        error: function () {
            console.log("Có lỗi xảy ra khi hiển thị số lượng sản phẩm.");
        }
    });
}

// Hàm xóa toàn bộ sản phẩm trong giỏ
function DeleteAll() {
    $.ajax({
        url: '/shoppingcart/DeleteAll',
        type: 'POST',
        success: function (rs) {
            if (rs && rs.Success) {
                LoadCart();
            }
        },
        error: function () {
            alert("Có lỗi xảy ra khi xóa toàn bộ giỏ hàng.");
        }
    });
}

// Hàm cập nhật số lượng sản phẩm trong giỏ
function Update(id, quantity) {
    $.ajax({
        url: '/shoppingcart/Update',
        type: 'POST',
        data: { id: id, quantity: quantity },
        success: function (rs) {
            if (rs && rs.Success) {
                LoadCart();
            }
        },
        error: function () {
            alert("Có lỗi xảy ra khi cập nhật sản phẩm.");
        }
    });
}

// Hàm tải nội dung giỏ hàng
function LoadCart() {
    $.ajax({
        url: '/shoppingcart/Partial_Item_Cart',
        type: 'GET',
        success: function (rs) {
            $('#load_data').html(rs);
        },
        error: function () {
            console.log("Có lỗi xảy ra khi tải giỏ hàng.");
        }
    });
}
