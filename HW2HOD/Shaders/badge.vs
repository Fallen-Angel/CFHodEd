vs_1_1

; this shader was created to fix vertically flipped (?) badge textures.
; 29 instruction slots used.

dcl_position v0
dcl_normal v1
dcl_color v2
dcl_texcoord0 v3

def c95, 0, 0.25, 0.5, 1

; perform transform.
m4x4 oPos, v0, c20

; copy texture co-ordinates.
mov oT0, v3

; copy inverted texture co-ordinates.
mov oT1.xzw, v3.xzw
sub oT1.y, c95.w, v3.y

; perform lighting.
; perform ambient lighting.
mov r0, c44
add r8, r0, c50
mul r8, r8, c36

; perform diffuse lighting with 0th light; assuming it to be directional.
dp3 r7.x, v1, -c49
mul r9, v2, c51

; perform specular lighting with 0th light.
mov r0, c38
mul r10, r0, c52

; first calculate the half-way vector.
sub r0, c32, v0
dp3 r1, r0, r0
rsq r1.x, r1.y
mul r0, r0, r1.x

sub r0, r0, c49
dp3 r1, r0, r0
rsq r1.x, r1.y
mul r0, r0, r1.x

; now perform specular lighting with 0th light.
dp3 r7.y, v1, r0
mov r7.w, c40.x

; execute lighting instruction
lit r7, r7

; perform emissive lighting.
mov r11, c39

; calculate colour 0: ambient + diffuse + emissive
add r8, r8, r11
mad oD0, r7.y, r9, r8

; calculate colour 1: specular
mul oD1, r7.z, r10
